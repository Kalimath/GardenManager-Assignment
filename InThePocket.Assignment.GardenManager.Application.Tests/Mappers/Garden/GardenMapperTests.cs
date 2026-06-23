using InThePocket.Assignment.GardenManager.Application.Mappers.Garden;
using InThePocket.Assignment.GardenManager.Application.Models.Identity;
using InThePocket.Assignment.GardenManager.Contracts.Api.Dto;

namespace InThePocket.Assignment.GardenManager.Application.Tests.Mappers.Garden;

public class GardenMapperTests
{
    private static readonly Guid SomeUserId = Guid.NewGuid();

    private static readonly User SomeUser = new()
    {
        Id = SomeUserId,
        FirstName = "John",
        LastName = "Doe",
        Age = 34,
        Email = "john.doe@email.com"
    };
    private static readonly GardenDto SomeGardenDto = new()
    {
        GardenName = "Test Garden",
        TotalSurfaceArea = 23.5,
        LocationDescription = "Test Location",
        TargetHumidityLevel = 55,
        UserId = SomeUserId
    };

    private static readonly Models.Garden SomeGarden = new()
    {
        GardenName = "Test Garden",
        TotalSurfaceArea = 23.5,
        LocationDescription = "Test Location",
        TargetHumidityLevel = 55,
        UserId = SomeUserId
    };

    private readonly GardenMapper _gardenMapper = new();

    [Fact]
    public void MapToModel_GivenGardenDto_ReturnsCorrectGardenModel()
    {
        var result = _gardenMapper.MapToModel(SomeGardenDto, SomeUser);
        
        Assert.Equivalent(SomeGarden, result);
    }
    
    [Fact]
    public void MapToDto_GivenGardenModel_ReturnsCorrectGardenDto()
    {
        var result = _gardenMapper.MapToDto(SomeGarden);
        
        Assert.Equivalent(SomeGardenDto, result);
    }
}