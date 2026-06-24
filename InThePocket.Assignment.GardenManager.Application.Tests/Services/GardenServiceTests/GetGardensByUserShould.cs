using System.Linq.Expressions;
using InThePocket.Assignment.GardenManager.Application.Domain.Models;
using InThePocket.Assignment.GardenManager.Application.Domain.Models.Identity;
using NSubstitute;

namespace InThePocket.Assignment.GardenManager.Application.Tests.Services.GardenServiceTests;

public class GetGardenByUserShould : GardenServiceTestBase
{
    [Fact]
    public async Task CallUserRepository_UserExists()
    {
        await GardenService.GetGardensByUser(SomeUserId);
        
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

        Task Act() => GardenService.GetGardensByUser(SomeUserId);
        
        var exception = await Assert.ThrowsAsync<ArgumentException>(Act);
        Assert.Equal("User with given id does not exist", exception.Message);
    }
    
    [Fact]
    public async Task CallGardenRepository_GetAll()
    {
        _ = await GardenService.GetGardensByUser(SomeUserId);
        
        await GardenRepository
            .Received(1)
            .GetList(Arg.Any<Expression<Func<Garden,bool>>>());
    }
    
    [Fact]
    public async Task CallGardenDtoMapperForEveryModel_WhenGardenModelsNotNull()
    {
        Garden[] gardenModels = [SomeGarden, SomeOtherGarden];
        GardenRepository
            .GetList(Arg.Any<Expression<Func<Garden,bool>>>())
            .Returns(gardenModels);
        
        _ = await GardenService.GetGardensByUser(SomeUserId);
        
        GardenMapper
            .Received(1)
            .MapToDto(SomeGarden);
        GardenMapper
            .Received(1)
            .MapToDto(SomeOtherGarden);
    }
    
    [Fact]
    public async Task ReturnGardenDto_WhenSuccessfullyMapped()
    {
        var result = await GardenService.GetGardenByReference(SomeGardenReference);
        
        Assert.Equivalent(SomeGardenDto, result);
    }
    
    [Fact]
    public async Task ReturnEmptyList_WhenNoGardenModelsFound()
    {
        var result = await GardenService.GetGardensByUser(SomeUserId);
        
        Assert.Empty(result);
    }
}