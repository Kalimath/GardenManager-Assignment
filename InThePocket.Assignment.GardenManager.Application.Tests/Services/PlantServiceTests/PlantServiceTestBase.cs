using InThePocket.Assignment.GardenManager.Application.Domain.Models;
using InThePocket.Assignment.GardenManager.Application.Mappers.Plant;
using InThePocket.Assignment.GardenManager.Application.Services.Garden;
using InThePocket.Assignment.GardenManager.Application.Services.Plant;
using InThePocket.Assignment.GardenManager.Application.Shared;
using InThePocket.Assignment.GardenManager.Contracts.Api;
using InThePocket.Assignment.GardenManager.Contracts.Api.Dto;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace InThePocket.Assignment.GardenManager.Application.Tests.Services.PlantServiceTests;

public class PlantServiceTestBase
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
    
    protected readonly PlantService PlantService;

    public PlantServiceTestBase()
    {
        var plantRepository = Substitute.For<IRepository<Plant>>();
        var rpmdService = Substitute.For<IRealtimePlantMetricDataService>();
        var gardenService = Substitute.For<IGardenService>();
        var plantMapper = Substitute.For<IPlantMapper>();
        var rpmdMapper = Substitute.For<IRealtimePlantMetricDataMapper>();
        var logger = Substitute.For<ILogger<PlantService>>();
        
        PlantService = new PlantService(plantRepository, gardenService, rpmdService, plantMapper, rpmdMapper, logger);
    }
}