using InThePocket.Assignment.GardenManager.Contracts.Api;
using InThePocket.Assignment.GardenManager.Contracts.Api.Dto;
using Microsoft.AspNetCore.Mvc;

namespace InThePocket.Assignment.GardenManager.Ports.Tests.Adapters.API.Controllers.PlantControllerTests;

public class CreateShould : PlantControllerTestBase
{
    [Fact]
    public async Task CallPlantDtoValidator()
    {
        _ = await PlantController.Create(SomePlantDto);
        
        await PlantDtoValidator
            .Received(1)
            .ValidateAsync(Arg.Any<PlantDto>());
    }
    
    [Fact]
    public async Task ReturnBadRequestWithErrors_WhenPlantDtoNotValid()
    {
        var invalidPlantDto = new PlantDto
        {
            PlantId = Guid.Empty,
            PlantName = string.Empty,
            Species = string.Empty,
            PlantType = (PlantType)999,
            PlantationDate = DateTime.Today.AddDays(1),
            SurfaceAreaRequired = -2.5,
            GardenId = Guid.Empty
        };
        var validationResult = new FluentValidation.Results.ValidationResult
        {
            Errors = [
                new FluentValidation.Results.ValidationFailure {PropertyName = "PlantId"},
                new FluentValidation.Results.ValidationFailure {PropertyName = "PlantName"},
                new FluentValidation.Results.ValidationFailure {PropertyName = "Species"},
                new FluentValidation.Results.ValidationFailure {PropertyName = "PlantType"},
                new FluentValidation.Results.ValidationFailure {PropertyName = "PlantationDate"},
                new FluentValidation.Results.ValidationFailure {PropertyName = "SurfaceAreaRequired"},
                new FluentValidation.Results.ValidationFailure {PropertyName = "IdealHumidityLevel"},
                new FluentValidation.Results.ValidationFailure {PropertyName = "GardenId"},
            ]
        };
        PlantDtoValidator
            .ValidateAsync(invalidPlantDto)
            .Returns(validationResult);
        
        var result = await PlantController.Create(invalidPlantDto);

        var response = Assert.IsType<BadRequestObjectResult>(result);
        Assert.NotNull(response.Value);
        Assert.False(((FluentValidation.Results.ValidationResult)response.Value).IsValid);
        Assert.Equal(8, ((FluentValidation.Results.ValidationResult)response.Value).Errors.Count);
    }
    
    [Fact]
    public async Task CallPlantService_WhenDataValid()
    {
        _ = await PlantController.Create(SomePlantDto);
        
        await PlantService
            .Received(1)
            .AddPlant(SomePlantDto);
    }
    
    [Fact]
    public async Task ReturnCreated_WhenDataValid()
    {
        var result = await PlantController.Create(SomePlantDto);

        Assert.IsType<CreatedResult>(result);
    }
}