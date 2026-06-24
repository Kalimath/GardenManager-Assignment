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
    protected static readonly PlantDto SomePlantDto = new()
    {
        PlantId = Guid.NewGuid(),
        PlantName = "Bird of Paradise",
        Species = "Strelitzia reginae",
        PlantType = PlantType.Flower,
        PlantationDate = DateTime.Today.AddDays(-30),
        SurfaceAreaRequired = 1.5,
        IdealHumidityLevel = 60,
        RealtimePlantMetricData = new RealtimePlantMetricDataDto(),
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
    }
}