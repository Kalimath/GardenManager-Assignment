using System.Linq.Expressions;
using InThePocket.Assignment.GardenManager.Application.Models;
using NSubstitute;

namespace InThePocket.Assignment.GardenManager.Application.Tests.Services.GardenServiceTests;

public class GetGardenOfUserByIdShould : GardenServiceTestBase
{
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