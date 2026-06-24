using Microsoft.AspNetCore.Mvc;

namespace InThePocket.Assignment.GardenManager.Ports.Tests.Adapters.API.Controllers.PlantControllerTests;

public class DeleteShould : PlantControllerTestBase
{
    [Fact]
    public async Task CallPlantService_RemovePlant()
    {
        var result = await PlantController.Delete(SomePlantId);

        await PlantService
            .Received(1)
            .RemovePlant(SomePlantId);
    }
    
    [Fact]
    public async Task ReturnAccepted()
    {
        PlantService
            .GetPlantById(SomePlantId)
            .Returns(SomePlantDto);

        var result = await PlantController.Delete(SomePlantId);

        Assert.IsType<AcceptedResult>(result);
    }
}