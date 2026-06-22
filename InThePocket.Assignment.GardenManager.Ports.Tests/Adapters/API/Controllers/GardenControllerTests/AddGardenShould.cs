using FluentValidation.Results;
using InThePocket.Assignment.GardenManager.Contracts.Dto;
using Microsoft.AspNetCore.Mvc;

namespace InThePocket.Assignment.GardenManager.Ports.Tests.Adapters.API.Controllers.GardenControllerTests;

public class AddGardenShould : GardenControllerTestBase
{
    [Fact]
    public async Task CallGardenDtoValidator()
    {
        _ = await GardenController.AddGarden(ValidGardenDto);
        
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
            LocationDescription = string.Empty
        };
        var validationResult = new ValidationResult
        {
            Errors = [
                new ValidationFailure {PropertyName = "GardenName"},
                new ValidationFailure {PropertyName = "TotalSurfaceArea"},
                new ValidationFailure {PropertyName = "LocationDescription"}
            ]
        };
        GardenDtoValidator
            .ValidateAsync(invalidGardenDto)
            .Returns(validationResult);
        
        var result = await GardenController.AddGarden(invalidGardenDto);

        var response = Assert.IsType<BadRequestObjectResult>(result);
        Assert.NotNull(response.Value);
        Assert.False(((ValidationResult)response.Value).IsValid);
        Assert.Equal(3, ((ValidationResult)response.Value).Errors.Count);
    }

    [Fact]
    public async Task CallGardenService_WhenDataValid()
    {
        _ = await GardenController.AddGarden(ValidGardenDto);

        await GardenService
            .Received(1)
            .AddGarden(ValidGardenDto);
    }
}