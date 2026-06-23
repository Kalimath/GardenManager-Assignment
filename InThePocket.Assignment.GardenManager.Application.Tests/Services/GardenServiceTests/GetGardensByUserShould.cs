using System.Linq.Expressions;
using InThePocket.Assignment.GardenManager.Application.Models;
using NSubstitute;

namespace InThePocket.Assignment.GardenManager.Application.Tests.Services.GardenServiceTests;

public class GetGardenByUserShould : GardenServiceTestBase
{
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