using InThePocket.Assignment.GardenManager.Contracts.Api;
using Microsoft.AspNetCore.Mvc;

namespace InThePocket.Assignment.GardenManager.Ports.Tests.Adapters.API.Controllers.GardenControllerTests;

public class GetShould : GardenControllerTestBase
{
    [Theory]
    [InlineData ("b3c1a8f4-5d6e-4a7b-9c2e-8f3d7a6e1c4f", "00000000-0000-0000-0000-000000000000")]
    [InlineData ("00000000-0000-0000-0000-000000000000", "b3c1a8f4-5d6e-4a7b-9c2e-8f3d7a6e1c4f")]
    public async Task ReturnBadRequest_WhenReferenceInvalid(Guid gardenId, Guid userId)
    {
        var invalidReference = new GardenReference {GardenId = gardenId, UserId = userId};
        
        var result = await GardenController.Get(invalidReference);
        
        var response = Assert.IsType<BadRequestObjectResult>(result);
        Assert.EndsWith("cannot be empty", response.Value as string);
    }
    
    [Fact]
    public async Task CallGardenService_WhenIdIsValid()
    {
        var someGardenId = Guid.NewGuid();
        
        _ = await GardenController.Get(SomeGardenReference);
        
        await GardenService
            .Received(1)
            .GetGardenByReference(SomeGardenReference);
    }
    
    [Fact]
    public async Task ReturnOkWithData_WhenIdIsValid()
    {
        var someGardenId = SomeGardenDtoWithId.GardenId;
        GardenService
            .GetGardenByReference(SomeGardenReference)
            .Returns(SomeGardenDtoWithId);
        
        var result = await GardenController.Get(SomeGardenReference);
        
        var response = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(SomeGardenDtoWithId, response.Value);
    }
}