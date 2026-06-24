using InThePocket.Assignment.GardenManager.Contracts.Api.Dto;
using Microsoft.AspNetCore.Mvc;

namespace InThePocket.Assignment.GardenManager.Ports.Tests.Adapters.API.Controllers.PlantControllerTests;

public class GetAllShould : PlantControllerTestBase
{
    [Fact]
    public async Task CallPlantService_GetAllPlants()
    {
        var result = await PlantController.GetAll();

        await PlantService
            .Received(1)
            .GetAllPlants();
    }
    
    [Fact]
    public async Task ReturnOkResultWithListOfPlants()
    {
        var plants = new[] { SomePlantDto, SomeOtherPlantDto };
        PlantService.GetAllPlants().Returns(plants);

        var result = await PlantController.GetAll();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var data = Assert.IsAssignableFrom<IEnumerable<PlantDto>>(okResult.Value);
        Assert.Equivalent(plants, data);
    }
}