using InThePocket.Assignment.GardenManager.Application.Models;

namespace InThePocket.Assignment.GardenManager.Application.Services.Garden;

public interface IPlantService
{
    Task AddPlant(PlantDto plantDto);
}