using InThePocket.Assignment.GardenManager.Application.Mappers.Garden;
using InThePocket.Assignment.GardenManager.Contracts.Dto;

namespace InThePocket.Assignment.GardenManager.Application.Services.Garden;

public class GardenService(IGardenMapper gardenMapper, IRepository<Models.Garden> gardenRepository) : IGardenService
{

    public async Task AddGarden(GardenDto gardenDto)
    {
        var model = gardenMapper.MapToModel(gardenDto);

        gardenRepository.Add(model);
        await gardenRepository.SaveChangesAsync();
    }
}