using FluentValidation.Results;
using InThePocket.Assignment.GardenManager.Contracts.Dto;
using Microsoft.AspNetCore.Mvc;

namespace InThePocket.Assignment.GardenManager.Ports.Tests.Adapters.API.Controllers.GardenControllerTests;

public class CreateShould : GardenControllerTestBase
{
    [Fact]
    public async Task CallGardenDtoValidator()
    {
        _ = await GardenController.Create(SomeValidGardenDto);
        
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
        
        var result = await GardenController.Create(invalidGardenDto);

        var response = Assert.IsType<BadRequestObjectResult>(result);
        Assert.NotNull(response.Value);
        Assert.False(((ValidationResult)response.Value).IsValid);
        Assert.Equal(5, ((ValidationResult)response.Value).Errors.Count);
    }

    [Fact]
    public async Task CallGardenService_WhenDataValid()
    {
        _ = await GardenController.Create(SomeValidGardenDto);

        await GardenService
            .Received(1)
            .AddGarden(SomeValidGardenDto);
    }
}