using System.Linq.Expressions;
using InThePocket.Assignment.GardenManager.Application.Mappers.Garden;
using InThePocket.Assignment.GardenManager.Application.Models;
using InThePocket.Assignment.GardenManager.Application.Models.Identity;
using InThePocket.Assignment.GardenManager.Application.Services.Garden;
using InThePocket.Assignment.GardenManager.Application.Shared;
using InThePocket.Assignment.GardenManager.Contracts.Api;
using InThePocket.Assignment.GardenManager.Contracts.Api.Dto;
using NSubstitute;

namespace InThePocket.Assignment.GardenManager.Application.Tests.Services.GardenServiceTests;

public class GardenServiceTestBase
{
    protected static readonly Guid SomeUserId = Guid.NewGuid();
    protected static readonly Guid SomeGardenId = Guid.NewGuid();
    protected static readonly Guid SomeOtherGardenId = Guid.NewGuid();
    
    protected static readonly GardenReference SomeGardenReference = new()
    {
        GardenId = SomeGardenId,
        UserId = SomeUserId
    };
    protected static readonly User SomeUser = new()
    {
        Id = SomeUserId,
        FirstName = "John",
        LastName = "Doe",
        Age = 34,
        Email = "john.doe@email.com"
    };
    protected static readonly GardenDto SomeGardenDto = new()
    {
        GardenId = SomeGardenId,
        GardenName = "Test Garden",
        TotalSurfaceArea = 23.5,
        LocationDescription = "Test Location",
        TargetHumidityLevel = 55,
        UserId = SomeUserId
    };

    protected static readonly Garden SomeGarden = new()
    {
        GardenId = SomeGardenId,
        GardenName = "Test Garden",
        TotalSurfaceArea = 23.5,
        LocationDescription = "Test Location",
        TargetHumidityLevel = 55,
        User = SomeUser
    };

    protected static readonly Garden SomeOtherGarden = new()
    {
        GardenId = SomeOtherGardenId,
        GardenName = "Test Garden 2",
        TotalSurfaceArea = 24.3,
        LocationDescription = "Test Location 2",
        TargetHumidityLevel = 17,
        User = SomeUser
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
            .MapToModel(SomeGardenDto, SomeUser)
            .Returns(SomeGarden);
        GardenMapper
            .MapToDto(SomeGarden)
            .Returns(SomeGardenDto);
        
        UserRepository
            .Get(Arg.Any<Expression<Func<User,bool>>>())
            .Returns(SomeUser);

        GardenRepository
            .Get(Arg.Any<Expression<Func<Garden, bool>>>())
            .Returns(SomeGarden);
    }

}