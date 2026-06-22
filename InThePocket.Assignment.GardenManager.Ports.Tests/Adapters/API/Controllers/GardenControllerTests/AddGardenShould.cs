using InThePocket.Assignment.GardenManager.Contracts.Dto;
using Microsoft.AspNetCore.Mvc;

namespace InThePocket.Assignment.GardenManager.Ports.Tests.Adapters.API.Controllers.GardenControllerTests;

public class AddGardenShould : GardenControllerTestBase
{
    [Fact]
    public async Task CallGardenDtoValidator()
    {
        _ = await GardenController.AddGarden(ValidGardenDto);
        
        await GardenDtoValidator
            .Received(1)
            .ValidateAsync(Arg.Any<GardenDto>());
    }
    
    [Fact]
    public async Task ReturnBadRequest_WhenGardenDtoNull()
    {
        var result = await GardenController.AddGarden(null!);
        
        Assert.IsType<BadRequestResult>(result);
    }
}