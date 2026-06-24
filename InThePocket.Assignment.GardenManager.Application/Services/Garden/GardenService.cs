using InThePocket.Assignment.GardenManager.Application.Domain.Models.Identity;
using InThePocket.Assignment.GardenManager.Application.Mappers.Garden;
using InThePocket.Assignment.GardenManager.Application.Shared;
using InThePocket.Assignment.GardenManager.Contracts.Api;
using InThePocket.Assignment.GardenManager.Contracts.Api.Dto;
using Microsoft.Extensions.Logging;

namespace InThePocket.Assignment.GardenManager.Application.Services.Garden;

public class GardenService(
    IGardenMapper gardenMapper,
    IRepository<Domain.Models.Garden> gardenRepository,
    IRepository<User> userRepository,
    ILogger<GardenService> logger) : IGardenService
{

    public async Task AddGarden(GardenDto gardenDto)
    {
        await ThrowIfUserWithIdNotExists(gardenDto.UserId);

        var model = gardenMapper.MapToModel(gardenDto);

        gardenRepository.Add(model);
        await gardenRepository.SaveChangesAsync();
    }

    public async Task<GardenDto> GetGardenByReference(GardenReference reference)
    {
        await ThrowIfUserWithIdNotExists(reference.UserId);
        
        var gardenModel = await gardenRepository.Get(g => g.GardenId == reference.GardenId && g.User.Id == reference.UserId) 
                        ?? throw new NullReferenceException("The requested garden can not be found");
        
        return gardenMapper.MapToDto(gardenModel);
    }

    public async Task<GardenDto[]> GetGardensByUser(Guid userId)
    {
        await ThrowIfUserWithIdNotExists(userId);
        
        var gardenModels = await gardenRepository.GetList(g => g.User.Id == userId);
        
        return gardenModels.Select(gardenMapper.MapToDto).ToArray();
    }

    public async Task UpdateGarden(GardenDto updatedGardenDto)
    {
        await ThrowIfUserWithIdNotExists(updatedGardenDto.UserId);
        
        gardenRepository.Update(gardenMapper.MapToModel(updatedGardenDto));
        await gardenRepository.SaveChangesAsync();
    }

    public async Task RemoveGarden(GardenReference reference)
    {
        await ThrowIfUserWithIdNotExists(reference.UserId);

        var gardenToRemove =
            await gardenRepository.Get(g => g.GardenId == reference.GardenId && g.User.Id == reference.UserId) 
            ?? throw new NullReferenceException("The requested garden can not be found");
        
        gardenRepository.Delete(gardenToRemove);
        await gardenRepository.SaveChangesAsync();
        
        logger.LogInformation("Garden with id {1} has been deleted by {2}.", reference.GardenId, reference.UserId);
    }

    public async Task<double> GetFreeSurfaceAreaOfGardenWithId(Guid gardenId)
    {
        var garden = await gardenRepository.Get(g => g.GardenId == gardenId);
        if (garden == null)
            throw new NullReferenceException("Garden with id " + gardenId + " does not exist");
        
        return garden.TotalSurfaceArea - garden.Plants.Sum(p => p.SurfaceAreaRequired);
    }

    private async Task ThrowIfUserWithIdNotExists(Guid userId)
    {
        if (!await UserExists(userId))
            throw new ArgumentException("User with given id does not exist");
    }

    private async Task<bool> UserExists(Guid userId)
    {
        return await userRepository.Any(user => user.Id == userId);
    }
}