using InThePocket.Assignment.GardenManager.Application.Mappers.Garden;
using InThePocket.Assignment.GardenManager.Application.Models;
using InThePocket.Assignment.GardenManager.Application.Services.Garden;
using InThePocket.Assignment.GardenManager.Contracts.Dto;
using NSubstitute;

namespace InThePocket.Assignment.GardenManager.Application.Tests.Services.GardenServiceTests;

public class GardenServiceTestBase
{
    protected static readonly GardenDto SomeGardenDto = new()
    {
        GardenName = "Test Garden",
        TotalSurfaceArea = 23.5,
        LocationDescription = "Test Location",
        TargetHumidityLevel = 55
    };

    protected static readonly Garden SomeGarden = new()
    {
        GardenName = "Test Garden",
        TotalSurfaceArea = 23.5,
        LocationDescription = "Test Location",
        TargetHumidityLevel = 55
    };

    protected readonly GardenService GardenService;
    protected readonly IGardenMapper GardenMapper;
    protected readonly IRepository<Garden> GardenRepository;

    protected GardenServiceTestBase()
    {
        GardenMapper = Substitute.For<IGardenMapper>();
        GardenRepository = Substitute.For<IRepository<Garden>>();

        GardenService = new GardenService(GardenMapper, GardenRepository);
        
        //substitutes
        GardenMapper
            .MapToModel(SomeGardenDto)
            .Returns(SomeGarden);
    }

}