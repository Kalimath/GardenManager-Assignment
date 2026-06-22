using NSubstitute;

namespace InThePocket.Assignment.GardenManager.Application.Tests.Services.GardenServiceTests;

public class AddGardenShould : GardenServiceTestBase
{
    [Fact]
    public async Task CallGardenMapper()
    {
        await GardenService.AddGarden(SomeGardenDto);
        
        GardenMapper
            .Received(1)
            .MapToModel(SomeGardenDto);
    }
    
    [Fact]
    public async Task CallGardenRepository_WithMappedModel()
    {
        await GardenService.AddGarden(SomeGardenDto);
        
        GardenRepository
            .Received(1)
            .Add(Arg.Is(SomeGarden));
        await GardenRepository
            .Received(1)
            .SaveChangesAsync();
    }
}