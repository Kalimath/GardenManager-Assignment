using System.Linq.Expressions;
using InThePocket.Assignment.GardenManager.Application.Mappers.Garden;
using InThePocket.Assignment.GardenManager.Application.Models;
using InThePocket.Assignment.GardenManager.Application.Models.Identity;
using InThePocket.Assignment.GardenManager.Application.Services.Garden;
using InThePocket.Assignment.GardenManager.Application.Shared;
using InThePocket.Assignment.GardenManager.Contracts.Api;
using InThePocket.Assignment.GardenManager.Contracts.Api.Dto;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

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
    protected readonly ILogger<GardenService> Logger;

    protected GardenServiceTestBase()
    {
        GardenMapper = Substitute.For<IGardenMapper>();
        GardenRepository = Substitute.For<IRepository<Garden>>();
        UserRepository = Substitute.For<IRepository<User>>();
        Logger = Substitute.For<ILogger<GardenService>>();

        GardenService = new GardenService(GardenMapper, GardenRepository, UserRepository, Logger);
        
        //substitutes
        GardenMapper
            .MapToModel(SomeGardenDto)
            .Returns(SomeGarden);
        GardenMapper
            .MapToDto(SomeGarden)
            .Returns(SomeGardenDto);
        
        UserRepository
            .Get(Arg.Any<Expression<Func<User,bool>>>())
            .Returns(SomeUser);
        UserRepository
            .Any(Arg.Any<Expression<Func<User, bool>>>())
            .Returns(true);

        GardenRepository
            .Get(Arg.Any<Expression<Func<Garden, bool>>>())
            .Returns(SomeGarden);
    }
}

public class RemoveGardenShould : GardenServiceTestBase
{
    [Fact]
    public async Task CallUserRepository_UserExists()
    {
        await GardenService.RemoveGarden(SomeGardenReference);
        
        await UserRepository
            .Received(1)
            .Any(Arg.Any<Expression<Func<User, bool>>>());
    }
    
    [Fact]
    public async Task ThrowArgumentException_WhenUserDoesNotExist()
    {
        UserRepository
            .Any(Arg.Any<Expression<Func<User, bool>>>())
            .Returns(false);

        Task Act() => GardenService.RemoveGarden(SomeGardenReference);
        
        var exception = await Assert.ThrowsAsync<ArgumentException>(Act);
        Assert.Equal("User with given id does not exist", exception.Message);
    }
    
    [Fact]
    public async Task CallGardenRepository_Get()
    {
        await GardenService.RemoveGarden(SomeGardenReference);
        
        await GardenRepository
            .Received(1)
            .Get(Arg.Any<Expression<Func<Garden,bool>>>());
    }
    
    [Fact]
    public async Task ThrowNullReferenceException_WhenGardenDoesNotExist()
    {
        GardenRepository
            .Get(Arg.Any<Expression<Func<Garden, bool>>>())
            .ReturnsNull();

        Task Act() => GardenService.RemoveGarden(SomeGardenReference);
        
        var exception = await Assert.ThrowsAsync<NullReferenceException>(Act);
        Assert.Equal("The requested garden can not be found", exception.Message);
    }
   
    [Fact]
    public async Task CallGardenRepository_Delete()
    {
       await GardenService.RemoveGarden(SomeGardenReference);
        
       GardenRepository
            .Received(1)
            .Delete(Arg.Any<Garden>());
       await GardenRepository
            .Received(1)
            .SaveChangesAsync();
    }
    
    [Fact]
    public async Task LogDeletedGarden()
    {
        await GardenService.RemoveGarden(SomeGardenReference);
        
        Logger
            .ReceivedWithAnyArgs(1)
            .LogInformation(message: default);
    }
}