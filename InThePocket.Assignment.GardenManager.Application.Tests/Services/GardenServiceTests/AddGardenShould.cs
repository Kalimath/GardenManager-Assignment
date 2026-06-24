using System.Linq.Expressions;
using InThePocket.Assignment.GardenManager.Application.Domain.Models.Identity;
using NSubstitute;

namespace InThePocket.Assignment.GardenManager.Application.Tests.Services.GardenServiceTests;

public class AddGardenShould : GardenServiceTestBase
{
    [Fact]
    public async Task CallUserRepository_UserExists()
    {
        await GardenService.AddGarden(SomeGardenDto);
        
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

        Task Act() => GardenService.AddGarden(SomeGardenDto);
        
        var exception = await Assert.ThrowsAsync<ArgumentException>(Act);
        Assert.Equal("User with given id does not exist", exception.Message);
    }
    
    [Fact]
    public async Task CallGardenMapper()
    {
        await GardenService.AddGarden(SomeGardenDto);
        
        GardenMapper
            .Received(1)
            .MapToModel(SomeGardenDto);
    }
    
    [Fact]
    public async Task CallGardenRepository_WithMappedModel()
    {
        await GardenService.AddGarden(SomeGardenDto);
        
        GardenRepository
            .Received(1)
            .Add(Arg.Is(SomeGarden));
        await GardenRepository
            .Received(1)
            .SaveChangesAsync();
    }
}