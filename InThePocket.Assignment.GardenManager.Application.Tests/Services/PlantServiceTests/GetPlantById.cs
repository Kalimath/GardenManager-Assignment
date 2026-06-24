using NSubstitute;

namespace InThePocket.Assignment.GardenManager.Application.Tests.Services.PlantServiceTests;

public class GetPlantById : PlantServiceTestBase
{
    [Fact]
    public async Task ThrowNullReferenceException_WhenPlantNotFound()
    {
        Task Act() => PlantService.GetPlantById(Guid.NewGuid());
        
        await Assert.ThrowsAsync<NullReferenceException>(Act);
    }
    
    [Fact]
    public async Task CallRealtimePlantMetricDataService_GetRealtimePlantMetricDataByPlantId()
    {
        _ = PlantService.GetPlantById(SomePlantId);
        
        await RpmdService
            .Received(1)
            .GetRealtimePlantMetricDataByPlantId(SomePlantId);
    }
}