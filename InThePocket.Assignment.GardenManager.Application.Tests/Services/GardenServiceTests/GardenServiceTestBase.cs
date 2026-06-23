using System.Linq.Expressions;
using InThePocket.Assignment.GardenManager.Application.Mappers.Garden;
using InThePocket.Assignment.GardenManager.Application.Models;
using InThePocket.Assignment.GardenManager.Application.Models.Identity;
using InThePocket.Assignment.GardenManager.Application.Services.Garden;
using InThePocket.Assignment.GardenManager.Application.Shared;
using InThePocket.Assignment.GardenManager.Contracts.Dto;
using NSubstitute;

namespace InThePocket.Assignment.GardenManager.Application.Tests.Services.GardenServiceTests;

public class GardenServiceTestBase
{
    private static readonly Guid SomeUserId = Guid.NewGuid();

    private static readonly User SomeUser = new()
    {
        Id = Guid.NewGuid(),
        UserId = SomeUserId,
        FirstName = "John",
        LastName = "Doe",
        Age = 34,
        Email = "john.doe@email.com"
    };
    protected static readonly GardenDto SomeGardenDto = new()
    {
        GardenName = "Test Garden",
        TotalSurfaceArea = 23.5,
        LocationDescription = "Test Location",
        TargetHumidityLevel = 55,
        UserId = SomeUserId
    };

    protected static readonly Garden SomeGarden = new()
    {
        GardenName = "Test Garden",
        TotalSurfaceArea = 23.5,
        LocationDescription = "Test Location",
        TargetHumidityLevel = 55,
        User = null!
    };

    protected readonly GardenService GardenService;
    protected readonly IGardenMapper GardenMapper;
    protected readonly IRepository<Garden> GardenRepository;
    protected readonly IRepository<User> UserRepository;

    protected GardenServiceTestBase()
    {
        GardenMapper = Substitute.For<IGardenMapper>();
        GardenRepository = Substitute.For<IRepository<Garden>>();
        UserRepository = Substitute.For<IRepository<User>>();

        GardenService = new GardenService(GardenMapper, GardenRepository, UserRepository);
        
        //substitutes
        GardenMapper
            .MapToModel(SomeGardenDto)
            .Returns(SomeGarden);
        
        UserRepository
            .Get(Arg.Any<Expression<Func<User,bool>>>())
            .Returns(SomeUser);
    }

}