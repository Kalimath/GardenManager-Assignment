using System.Linq.Expressions;
using InThePocket.Assignment.GardenManager.Application.Mappers.Garden;
using InThePocket.Assignment.GardenManager.Application.Models.Identity;
using InThePocket.Assignment.GardenManager.Application.Shared;
using InThePocket.Assignment.GardenManager.Contracts.Api;
using InThePocket.Assignment.GardenManager.Contracts.Api.Dto;

namespace InThePocket.Assignment.GardenManager.Application.Services.Garden;

public class GardenService(IGardenMapper gardenMapper, IRepository<Models.Garden> gardenRepository, IRepository<User> userRepository) : IGardenService
{

    public async Task AddGarden(GardenDto gardenDto)
    {
        var currentUser = await GetCurrentUser(u => u.Id == gardenDto.UserId);

        var model = gardenMapper.MapToModel(gardenDto, currentUser);

        gardenRepository.Add(model);
        await gardenRepository.SaveChangesAsync();
    }

    public async Task<GardenDto> GetGardenByReference(GardenReference reference)
    {
        var gardenModel = await gardenRepository.Get(g => g.GardenId == reference.GardenId && g.User.Id == reference.UserId) 
                        ?? throw new NullReferenceException("The requested garden can not be found");

        return gardenMapper.MapToDto(gardenModel);
    }

    public Task<GardenDto[]> GetGardensByUser(Guid userId)
    {
        throw new NotImplementedException();
    }

    private async Task<User> GetCurrentUser(Expression<Func<User,bool>> predicate)
    {
        //Normally the user would be fetched from UserManager in the Ports layer. For the sake of this assignment, I fetch it from the repository.
        var currentUser = await userRepository.Get(predicate) 
                          ?? throw new NullReferenceException("The user for this garden could not be found");
        return currentUser;
    }
}