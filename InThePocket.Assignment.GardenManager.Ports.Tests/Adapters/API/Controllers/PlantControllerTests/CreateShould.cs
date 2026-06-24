using InThePocket.Assignment.GardenManager.Contracts.Api;
using InThePocket.Assignment.GardenManager.Contracts.Api.Dto;
using Microsoft.AspNetCore.Mvc;

namespace InThePocket.Assignment.GardenManager.Ports.Tests.Adapters.API.Controllers.PlantControllerTests;

public class CreateShould : PlantControllerTestBase
{
    protected static readonly RealtimePlantMetricDataDto SomeCreateRpmdDto = new()
    {
        // no RealtimePlantMetricDataId at creation
        CurrentHumidityLevel = 55,
        LastIrrigationStartTime = DateTime.UtcNow.AddHours(-2),
        LastIrrigationEndTime = DateTime.UtcNow.AddHours(-1)
        // no plantId at creation
    };
    protected static readonly PlantDto SomeCreatePlantDto = new()
    {
        // no plantId at creation
        PlantName = "Bird of Paradise",
        Species = "Strelitzia reginae",
        PlantType = PlantType.Flower,
        PlantationDate = DateTime.Today.AddDays(-30),
        SurfaceAreaRequired = 1.5,
        IdealHumidityLevel = 60,
        RealtimePlantMetricData = SomeCreateRpmdDto,
        GardenId = SomeGardenId
    };
    
    [Fact]
    public async Task CallPlantDtoValidator()
    {
        _ = await PlantController.Create(SomeCreatePlantDto);
        
        await PlantDtoValidator
            .Received(1)
            .ValidateAsync(SomeCreatePlantDto);
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
            RealtimePlantMetricData = SomeRpmdDto,
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
    }
    
    [Fact]
    public async Task CallPlantService_WhenDataValid()
    {
        _ = await PlantController.Create(SomeCreatePlantDto);
        
        await PlantService
            .Received(1)
            .AddPlant(Arg.Any<PlantDto>());
    }
    
    [Fact]
    public async Task ReturnCreated_WhenDataValid()
    {
        var result = await PlantController.Create(SomeCreatePlantDto);

        Assert.IsType<CreatedResult>(result);
    }
}