using FluentValidation;
using InThePocket.Assignment.GardenManager.Application.Services.Plant;
using InThePocket.Assignment.GardenManager.Contracts.Api;
using InThePocket.Assignment.GardenManager.Contracts.Api.Dto;
using InThePocket.Assignment.GardenManager.Ports.Adapters.API.Controllers;
using Microsoft.Extensions.Logging;

namespace InThePocket.Assignment.GardenManager.Ports.Tests.Adapters.API.Controllers.PlantControllerTests;

public class PlantControllerTestBase
{
    protected static readonly Guid SomeGardenId = Guid.NewGuid();
    protected static readonly Guid SomePlantId = Guid.NewGuid();
    private static readonly Guid SomeOtherPlantId = Guid.NewGuid();
    protected static readonly RealtimePlantMetricDataDto SomeRpmdDto = new()
    {
        RealtimePlantMetricDataId = Guid.NewGuid(),
        CurrentHumidityLevel = 55,
        LastIrrigationStartTime = DateTime.UtcNow.AddHours(-2),
        LastIrrigationEndTime = DateTime.UtcNow.AddHours(-1),
        PlantId = SomePlantId
    };
    protected static readonly PlantDto SomePlantDto = new()
    {
        PlantId = SomePlantId,
        PlantName = "Bird of Paradise",
        Species = "Strelitzia reginae",
        PlantType = PlantType.Flower,
        PlantationDate = DateTime.Today.AddDays(-30),
        SurfaceAreaRequired = 1.5,
        IdealHumidityLevel = 60,
        RealtimePlantMetricData = SomeRpmdDto,
        GardenId = SomeGardenId
    };
    
    protected static readonly RealtimePlantMetricDataDto SomeOtherRpmdDto = new()
    {
        RealtimePlantMetricDataId = Guid.NewGuid(),
        CurrentHumidityLevel = 55,
        LastIrrigationStartTime = DateTime.UtcNow.AddHours(-2),
        LastIrrigationEndTime = DateTime.UtcNow.AddHours(-1),
        PlantId = SomeOtherPlantId
    };
    protected static readonly PlantDto SomeOtherPlantDto = new()
    {
        PlantId = SomeOtherPlantId,
        PlantName = "some other plant",
        Species = "some other species",
        PlantType = PlantType.Vegetable,
        PlantationDate = DateTime.Today.AddDays(-20),
        SurfaceAreaRequired = 1.6,
        IdealHumidityLevel = 30,
        RealtimePlantMetricData = SomeOtherRpmdDto,
        GardenId = SomeGardenId
    };
    
    protected readonly IPlantController PlantController;
    protected readonly IValidator<PlantDto> PlantDtoValidator;
    protected readonly IValidator<RealtimePlantMetricDataDto> RpmdDtoValidator;
    protected readonly IPlantService PlantService;

    protected PlantControllerTestBase()
    {
        PlantDtoValidator = Substitute.For<IValidator<PlantDto>>();
        RpmdDtoValidator = Substitute.For<IValidator<RealtimePlantMetricDataDto>>();
        PlantService = Substitute.For<IPlantService>();
        var logger = Substitute.For<ILogger<PlantController>>();
        
        PlantController = new PlantController(PlantDtoValidator, RpmdDtoValidator, PlantService, logger);
        
        //substitutes
        var validValidationResult = new FluentValidation.Results.ValidationResult();
        PlantDtoValidator
            .ValidateAsync(Arg.Any<PlantDto>())
            .Returns(validValidationResult);
        RpmdDtoValidator
            .ValidateAsync(Arg.Any<RealtimePlantMetricDataDto>())
            .Returns(validValidationResult);
    }
}