using InThePocket.Assignment.GardenManager.Contracts.Api.Dto;
using Microsoft.AspNetCore.Mvc;

namespace InThePocket.Assignment.GardenManager.Ports.Tests.Adapters.API.Controllers.PlantControllerTests;

public class GetShould : PlantControllerTestBase
{
    [Fact]
    public async Task CallPlantService_GetPlantById()
    {
        _ = await PlantController.Get(SomePlantId);

        await PlantService
            .Received(1)
            .GetPlantById(SomePlantId);
    }
    
    [Fact]
    public async Task ReturnOkResultWithPlantDto()
    {
        PlantService
            .GetPlantById(SomePlantId)
            .Returns(SomePlantDto);

        var result = await PlantController.Get(SomePlantId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var plantDto = Assert.IsType<PlantDto>(okResult.Value);
        Assert.Equal(SomePlantDto.PlantId, plantDto.PlantId);
    }
}