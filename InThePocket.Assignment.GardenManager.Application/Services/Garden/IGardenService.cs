using InThePocket.Assignment.GardenManager.Contracts.Dto;

namespace InThePocket.Assignment.GardenManager.Application.Services.Garden;

public interface IGardenService
{
    Task AddGarden(GardenDto gardenDto);
    Task<GardenDto> GetGardenById(Guid gardenId);
}