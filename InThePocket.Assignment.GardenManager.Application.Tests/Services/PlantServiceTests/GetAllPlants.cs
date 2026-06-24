using System.Linq.Expressions;
using InThePocket.Assignment.GardenManager.Application.Domain.Models;
using NSubstitute;

namespace InThePocket.Assignment.GardenManager.Application.Tests.Services.PlantServiceTests;

public class GetAllPlants : PlantServiceTestBase
{
    [Fact]
    public async Task CallPlantRepository_GetList()
    {
        var result = await PlantService.GetAllPlants();

        await PlantRepository
            .Received(1)
            .GetList(Arg.Any<Expression<Func<Plant, bool>>>());
    }
    [Fact]
    public async Task ReturnEmpty_WhenRepositoryReturnsEmpty()
    {
        PlantRepository
            .GetList(Arg.Any<Expression<Func<Plant, bool>>>())
            .Returns(new List<Plant>());
        
        var result = await PlantService.GetAllPlants();

        Assert.Empty(result);
    }
}