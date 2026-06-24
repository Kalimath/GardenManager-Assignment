using System.Linq.Expressions;
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
    protected static readonly Guid SomePlantId = Guid.NewGuid();
    protected static readonly PlantDto SomePlantDto = new()
    {
        PlantId = SomePlantId,
        PlantName = "Bird of Paradise",
        Species = "Strelitzia reginae",
        PlantType = PlantType.Flower,
        PlantationDate = DateTime.Today.AddDays(-30),
        SurfaceAreaRequired = 1.5,
        IdealHumidityLevel = 60,
        RealtimePlantMetricData = new RealtimePlantMetricDataDto(),
        GardenId = SomeGardenId
    };
    protected static readonly Plant SomePlant = new()
    {
        PlantId = SomePlantId,
        PlantName = "Bird of Paradise",
        Species = "Strelitzia reginae",
        PlantType = PlantType.Flower,
        PlantationDate = DateTime.Today.AddDays(-30),
        SurfaceAreaRequired = 1.5,
        IdealHumidityLevel = 60,
        GardenId = SomeGardenId
    };
    
    protected readonly PlantService PlantService;
    protected readonly IGardenService GardenService;
    protected readonly IRepository<Plant> PlantRepository = Substitute.For<IRepository<Plant>>();
    protected readonly IRealtimePlantMetricDataService RpmdService;

    public PlantServiceTestBase()
    {
        RpmdService = Substitute.For<IRealtimePlantMetricDataService>();
        GardenService = Substitute.For<IGardenService>();
        var plantMapper = Substitute.For<IPlantMapper>();
        var rpmdMapper = Substitute.For<IRealtimePlantMetricDataMapper>();
        var logger = Substitute.For<ILogger<PlantService>>();
        
        PlantService = new PlantService(PlantRepository, GardenService, RpmdService, plantMapper, rpmdMapper, logger);
        
        //Substitutes
        PlantRepository
            .Get(Arg.Any<Expression<Func<Plant, bool>>>())
            .Returns(SomePlant);
    }
}

public class RemovePlantShould : PlantServiceTestBase
{
    [Fact]
    public async Task ThrowNullReferenceException_WhenPlantNotFound()
    {
        Task Act() => PlantService.GetPlantById(Guid.NewGuid());
        
        await Assert.ThrowsAsync<NullReferenceException>(Act);
    }
    
    [Fact]
    public async Task CallPlantRepository_WhenPlantFound()
    {
        await PlantService.RemovePlant(SomePlantId);
        
        PlantRepository
            .Received(1)
            .Delete(SomePlant);
        
        await PlantRepository
            .Received(1)
            .SaveChangesAsync();
    }
}