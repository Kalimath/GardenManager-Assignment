using InThePocket.Assignment.GardenManager.Contracts.Dto;

namespace InThePocket.Assignment.GardenManager.Application.Services;

public interface IGardenService
{
    Task AddGarden(GardenDto gardenDto);
}