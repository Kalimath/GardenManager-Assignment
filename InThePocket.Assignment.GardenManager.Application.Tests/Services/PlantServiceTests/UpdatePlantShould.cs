using InThePocket.Assignment.GardenManager.Contracts.Api;
using InThePocket.Assignment.GardenManager.Contracts.Api.Dto;
using NSubstitute;

namespace InThePocket.Assignment.GardenManager.Application.Tests.Services.PlantServiceTests;

public class UpdatePlantShould : PlantServiceTestBase
{
    [Fact]
    public async Task ThrowInvalidOperationException_WhenNotEnoughFreeSurfaceAreaInGarden()
    {
        var invalidPlantDto = new PlantDto
        {
            PlantId = Guid.NewGuid(),
            PlantName = "Large Plant",
            Species = "Ficus lyrata",
            PlantType = PlantType.Flower,
            PlantationDate = DateTime.Today,
            SurfaceAreaRequired = 2.1, // Requires more surface area than available
            IdealHumidityLevel = 60,
            RealtimePlantMetricData = new RealtimePlantMetricDataDto(),
            GardenId = SomeGardenId
        };
        GardenService
            .GetFreeSurfaceAreaOfGardenWithId(SomeGardenId)
            .Returns(2.0);
        
        Task Act() => PlantService.UpdatePlant(invalidPlantDto);
        
        await Assert.ThrowsAsync<InvalidOperationException>(Act);
    }
}