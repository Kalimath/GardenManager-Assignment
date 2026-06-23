using InThePocket.Assignment.GardenManager.Application.Mappers.Garden;
using InThePocket.Assignment.GardenManager.Application.Models.Identity;
using InThePocket.Assignment.GardenManager.Contracts.Dto;

namespace InThePocket.Assignment.GardenManager.Application.Tests.Mappers.Garden;

public class GardenMapperTests
{
    private static readonly Guid SomeUserId = Guid.NewGuid();

    protected static readonly User SomeUser = new()
    {
        Id = SomeUserId,
        FirstName = "John",
        LastName = "Doe",
        Age = 34,
        Email = "john.doe@email.com"
    };
    private static readonly GardenDto GardenDto = new()
    {
        GardenName = "Test Garden",
        TotalSurfaceArea = 23.5,
        LocationDescription = "Test Location",
        TargetHumidityLevel = 55
    };

    private static readonly Models.Garden Garden = new()
    {
        GardenName = "Test Garden",
        TotalSurfaceArea = 23.5,
        LocationDescription = "Test Location",
        TargetHumidityLevel = 55,
        User = SomeUser
    };

    private readonly GardenMapper _gardenMapper = new();

    [Fact]
    public void MapToModel_GivenGardenDto_ReturnsCorrectGardenModel()
    {
        var result = _gardenMapper.MapToModel(GardenDto, SomeUser);
        
        Assert.Equivalent(Garden, result);
    }
}