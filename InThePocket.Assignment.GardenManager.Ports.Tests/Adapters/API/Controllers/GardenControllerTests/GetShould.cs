using Microsoft.AspNetCore.Mvc;

namespace InThePocket.Assignment.GardenManager.Ports.Tests.Adapters.API.Controllers.GardenControllerTests;

public class GetShould : GardenControllerTestBase
{
    
    [Fact]
    public async Task ReturnBadRequest_WhenIdIsEmpty()
    {
        var result = await GardenController.Get(Guid.Empty);
        
        var response = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("GardenId cannot be empty", response.Value);
    }
    
    [Fact]
    public async Task CallGardenService_WhenIdIsValid()
    {
        var someGardenId = Guid.NewGuid();
        
        _ = await GardenController.Get(someGardenId);
        
        await GardenService
            .Received(1)
            .GetGardenById(someGardenId);
    }
    
    [Fact]
    public async Task ReturnOkWithData_WhenIdIsValid()
    {
        var someGardenId = SomeGardenDtoWithId.GardenId;
        GardenService
            .GetGardenById(someGardenId)
            .Returns(SomeGardenDtoWithId);
        
        var result = await GardenController.Get(someGardenId);
        
        var response = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(SomeGardenDtoWithId, response.Value);
    }
}