using InThePocket.Assignment.GardenManager.Contracts.Api;
using InThePocket.Assignment.GardenManager.Contracts.Api.Dto;

namespace InThePocket.Assignment.GardenManager.Application.Services.Garden;

public interface IGardenService
{
    Task AddGarden(GardenDto gardenDto);
    Task<GardenDto> GetGardenByReference(GardenReference reference);
    Task<GardenDto[]> GetGardensByUser(Guid userId);
    Task UpdateGarden(GardenDto updatedGardenDto);
    Task RemoveGarden(GardenReference reference);
}