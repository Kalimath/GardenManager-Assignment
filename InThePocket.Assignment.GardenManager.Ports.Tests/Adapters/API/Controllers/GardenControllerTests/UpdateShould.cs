using FluentValidation.Results;
using InThePocket.Assignment.GardenManager.Contracts.Api.Dto;
using Microsoft.AspNetCore.Mvc;

namespace InThePocket.Assignment.GardenManager.Ports.Tests.Adapters.API.Controllers.GardenControllerTests;

public class UpdateShould : GardenControllerTestBase
{
    [Fact]
    public async Task CallGardenDtoValidator()
    {
        _ = await GardenController.Update(SomeGardenDto);
        
        await GardenDtoValidator
            .Received(1)
            .ValidateAsync(Arg.Any<GardenDto>());
    }

    [Fact]
    public async Task ReturnBadRequestWithErrors_WhenGardenDtoNotValid()
    {
        var invalidGardenDto = new GardenDto
        {
            GardenId = Guid.Empty,
            GardenName = string.Empty,
            TotalSurfaceArea = -1.5,
            LocationDescription = string.Empty,
            TargetHumidityLevel = 155,
            UserId = Guid.Empty
        };
        var validationResult = new ValidationResult
        {
            Errors = [
                new ValidationFailure{PropertyName = "GardenId"},
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
        Assert.Equal(6, ((ValidationResult)response.Value).Errors.Count);
    }

    [Fact]
    public async Task CallGardenService_WhenDataValid()
    {
        _ = await GardenController.Update(SomeGardenDto);

        await GardenService
            .Received(1)
            .UpdateGarden(SomeGardenDto);
    }
    
    [Fact]
    public async Task ReturnAccepted_WhenUpdateOrderSuccessfully()
    {
        var result = await GardenController.Update(SomeGardenDto);

        var response = Assert.IsType<AcceptedResult>(result);
        Assert.Equal(202, response.StatusCode);
    }
}