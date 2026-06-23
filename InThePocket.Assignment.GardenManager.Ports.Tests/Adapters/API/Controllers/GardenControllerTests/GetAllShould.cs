using InThePocket.Assignment.GardenManager.Contracts.Api.Dto;
using Microsoft.AspNetCore.Mvc;
using NSubstitute.ExceptionExtensions;

namespace InThePocket.Assignment.GardenManager.Ports.Tests.Adapters.API.Controllers.GardenControllerTests;

public class GetAllShould : GardenControllerTestBase
{
    [Fact]
    public async Task ReturnBadRequest_WhenUserIdEmpty()
    {
        var result = await GardenController.GetAll(Guid.Empty);
        
        var response = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("UserId cannot be empty", response.Value);
    }
    
    [Fact]
    public async Task CallGardenService_WhenUserIdIsValid()
    {
        _ = await GardenController.GetAll(SomeUserId);
        
        await GardenService
            .Received(1)
            .GetGardensByUser(SomeUserId);
    }
    
    [Fact]
    public async Task ReturnOkWithData()
    {
        var someOtherGardenDtoWithId = new GardenDto
        {
            GardenId = Guid.NewGuid(),
            GardenName = "SomeOtherGarden",
            TargetHumidityLevel = 65,
            TotalSurfaceArea = 300,
            LocationDescription = "SomeOtherLocation",
            UserId = SomeUserId
        };
        GardenDto[] expected = [SomeGardenDtoWithId, someOtherGardenDtoWithId];
        GardenService
            .GetGardensByUser(SomeUserId)
            .Returns(expected);
        
        var result = await GardenController.GetAll(SomeUserId);
        
        var response = Assert.IsType<OkObjectResult>(result);
        Assert.Equivalent(expected, response.Value);
    }
    
    [Fact]
    public async Task ReturnEmpty_WhenNoGardenFound()
    {
        GardenService
            .GetGardensByUser(SomeUserId)
            .Returns([]);
        
        var result = await GardenController.GetAll(SomeUserId);
        
        var response = Assert.IsType<OkObjectResult>(result);
        Assert.Equivalent(Array.Empty<GardenDto>(), response.Value);
    }
    
    [Fact]
    public async Task ReturnProblem_WhenGardenServiceThrows()
    {
        var unknownUserId = Guid.NewGuid();
        GardenService
            .GetGardensByUser(unknownUserId)
            .Throws<NullReferenceException>();
        
        var result = await GardenController.GetAll(unknownUserId);
        
        var response = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, response.StatusCode);
    }
    
    
}