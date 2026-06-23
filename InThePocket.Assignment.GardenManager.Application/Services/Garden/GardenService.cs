using InThePocket.Assignment.GardenManager.Application.Mappers.Garden;
using InThePocket.Assignment.GardenManager.Application.Shared;
using InThePocket.Assignment.GardenManager.Contracts.Dto;

namespace InThePocket.Assignment.GardenManager.Application.Services.Garden;

public class GardenService(IGardenMapper gardenMapper, IRepository<Models.Garden> gardenRepository, IRepository<Models.Identity.User> userRepository) : IGardenService
{

    public async Task AddGarden(GardenDto gardenDto)
    {
        _ = await userRepository.Get(g => g.Id.Equals(gardenDto.UserId));
        
        var model = gardenMapper.MapToModel(gardenDto);

        gardenRepository.Add(model);
        await gardenRepository.SaveChangesAsync();
    }
}