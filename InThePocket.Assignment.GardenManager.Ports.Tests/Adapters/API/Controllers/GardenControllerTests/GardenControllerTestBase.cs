using FluentValidation;
using FluentValidation.Results;
using InThePocket.Assignment.GardenManager.Application.Services.Garden;
using InThePocket.Assignment.GardenManager.Contracts.Api;
using InThePocket.Assignment.GardenManager.Contracts.Api.Dto;
using InThePocket.Assignment.GardenManager.Ports.Adapters.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace InThePocket.Assignment.GardenManager.Ports.Tests.Adapters.API.Controllers.GardenControllerTests;

public class GardenControllerTestBase
{
    protected static readonly Guid SomeUserId = Guid.NewGuid();
    protected static readonly Guid SomeGardenId = Guid.NewGuid();

    protected static readonly GardenReference SomeGardenReference = new() { GardenId = SomeGardenId, UserId = SomeUserId };
    protected static readonly GardenDto SomeValidGardenDto = new()
    {
        GardenName = "some garden",
        TotalSurfaceArea = 100.1,
        LocationDescription = "some location description",
        TargetHumidityLevel = 55,
        UserId = SomeUserId
    };
    protected static readonly GardenDto SomeGardenDtoWithId = new()
    {
        GardenId = SomeGardenId,
        GardenName = "some garden",
        TotalSurfaceArea = 100.1,
        LocationDescription = "some location description",
        TargetHumidityLevel = 55,
        UserId = SomeUserId
    };
    
    protected readonly GardenController GardenController;
    protected readonly IValidator<GardenDto> GardenDtoValidator;
    protected readonly IGardenService GardenService;

    protected GardenControllerTestBase()
    {
        GardenDtoValidator = Substitute.For<IValidator<GardenDto>>();
        GardenService = Substitute.For<IGardenService>();
        
        GardenController = new GardenController(GardenDtoValidator, GardenService);
        
        //substitutes
        var validValidationResult = new FluentValidation.Results.ValidationResult();
        _ = GardenDtoValidator
            .ValidateAsync(Arg.Any<GardenDto>())
            .Returns(validValidationResult);
    }
}

public class UpdateShould : GardenControllerTestBase
{
    [Fact]
    public async Task CallGardenDtoValidator()
    {
        _ = await GardenController.Update(SomeValidGardenDto);
        
        await GardenDtoValidator
            .Received(1)
            .ValidateAsync(Arg.Any<GardenDto>());
    }

    [Fact]
    public async Task ReturnBadRequestWithErrors_WhenGardenDtoNotValid()
    {
        var invalidGardenDto = new GardenDto
        {
            GardenName = string.Empty,
            TotalSurfaceArea = -1.5,
            LocationDescription = string.Empty,
            TargetHumidityLevel = 155,
            UserId = Guid.Empty
        };
        var validationResult = new ValidationResult
        {
            Errors = [
                new ValidationFailure {PropertyName = "GardenName"},
                new ValidationFailure {PropertyName = "TotalSurfaceArea"},
                new ValidationFailure {PropertyName = "LocationDescription"},
                new ValidationFailure {PropertyName = "TargetHumidityLevel"},
                new ValidationFailure {PropertyName = "UserId"}
            ]
        };
        GardenDtoValidator
            .ValidateAsync(invalidGardenDto)
            .Returns(validationResult);
        
        var result = await GardenController.Update(invalidGardenDto);

        var response = Assert.IsType<BadRequestObjectResult>(result);
        Assert.NotNull(response.Value);
        Assert.False(((ValidationResult)response.Value).IsValid);
        Assert.Equal(5, ((ValidationResult)response.Value).Errors.Count);
    }

    [Fact]
    public async Task CallGardenService_WhenDataValid()
    {
        _ = await GardenController.Update(SomeValidGardenDto);

        await GardenService
            .Received(1)
            .UpdateGarden(SomeValidGardenDto);
    }
    
    [Fact]
    public async Task ReturnAccepted_WhenUpdateOrderSuccessfully()
    {
        var result = await GardenController.Update(SomeValidGardenDto);

        var response = Assert.IsType<AcceptedResult>(result);
        Assert.Equal(202, response.StatusCode);
    }
}