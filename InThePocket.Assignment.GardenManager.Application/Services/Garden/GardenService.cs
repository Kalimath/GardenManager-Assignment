using InThePocket.Assignment.GardenManager.Application.Mappers.Garden;
using InThePocket.Assignment.GardenManager.Application.Models.Identity;
using InThePocket.Assignment.GardenManager.Application.Shared;
using InThePocket.Assignment.GardenManager.Contracts.Dto;

namespace InThePocket.Assignment.GardenManager.Application.Services.Garden;

public class GardenService(IGardenMapper gardenMapper, IRepository<Models.Garden> gardenRepository, IRepository<User> userRepository) : IGardenService
{

    public async Task AddGarden(GardenDto gardenDto)
    {
        //Normally the user would be fetched from UserManager in the Ports layer. For the sake of this assignment, I fetch it from the repository.
        var currentUser = await userRepository.Get(g => g.Id.Equals(gardenDto.UserId)) 
            ?? throw new NullReferenceException("The user for this garden could not be found");
        
        var model = gardenMapper.MapToModel(gardenDto, currentUser);

        gardenRepository.Add(model);
        await gardenRepository.SaveChangesAsync();
    }

    public Task<GardenDto> GetGardenById(Guid gardenId)
    {
        throw new NotImplementedException();
    }
}