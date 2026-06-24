using System.Linq.Expressions;
using InThePocket.Assignment.GardenManager.Application.Domain.Models;
using InThePocket.Assignment.GardenManager.Application.Domain.Models.Identity;
using NSubstitute;

namespace InThePocket.Assignment.GardenManager.Application.Tests.Services.GardenServiceTests;

public class GetGardenOfUserByIdShould : GardenServiceTestBase
{
    [Fact]
    public async Task CallUserRepository_UserExists()
    {
        await GardenService.GetGardenByReference(SomeGardenReference);
        
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

        Task Act() => GardenService.GetGardenByReference(SomeGardenReference);
        
        var exception = await Assert.ThrowsAsync<ArgumentException>(Act);
        Assert.Equal("User with given id does not exist", exception.Message);
    }
    
    [Fact]
    public async Task CallGardenRepository_Get()
    {
        _ = await GardenService.GetGardenByReference(SomeGardenReference);
        
        await GardenRepository
            .Received(1)
            .Get(Arg.Any<Expression<Func<Garden,bool>>>());
    }
    
    [Fact]
    public async Task CallGardenDtoMapper_WhenGardenModelNotNull()
    {
        _ = await GardenService.GetGardenByReference(SomeGardenReference);
        
        GardenMapper
            .Received(1)
            .MapToDto(SomeGarden);
    }
    
    [Fact]
    public async Task ReturnGardenDto_WhenSuccessfullyMapped()
    {
        var result = await GardenService.GetGardenByReference(SomeGardenReference);
        
        Assert.Equivalent(SomeGardenDto, result);
    }
}