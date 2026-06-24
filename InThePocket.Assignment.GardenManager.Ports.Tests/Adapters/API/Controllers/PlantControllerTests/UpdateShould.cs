using Microsoft.AspNetCore.Mvc;

namespace InThePocket.Assignment.GardenManager.Ports.Tests.Adapters.API.Controllers.PlantControllerTests;

public class UpdateShould : PlantControllerTestBase
{
    [Fact]
    public async Task CallPlantService()
    {
        _ = await PlantController.Update(SomePlantDto);
        
        await PlantService
            .Received(1)
            .UpdatePlant(SomePlantDto);
    }
    
    [Fact]
    public async Task ReturnAccepted_WhenPlantIsValid()
    {
        var result = await PlantController.Update(SomeOtherPlantDto);

        Assert.IsType<AcceptedResult>(result);
    }
}