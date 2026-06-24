using InThePocket.Assignment.GardenManager.Application.Mappers.Plant;
using InThePocket.Assignment.GardenManager.Application.Services.Garden;
using InThePocket.Assignment.GardenManager.Application.Shared;
using InThePocket.Assignment.GardenManager.Contracts.Api.Dto;
using Microsoft.Extensions.Logging;

namespace InThePocket.Assignment.GardenManager.Application.Services.Plant;

public class PlantService(
    IRepository<Domain.Models.Plant> plantRepository,
    IGardenService gardenService,
    IRealtimePlantMetricDataService rpmdService,
    IPlantMapper plantMapper,
    ILogger<PlantService> logger) : IPlantService
{

    public async Task AddPlant(PlantDto plantDto)
    {
        await ThrowIfNotEnoughFreeSurfaceAreaInGarden(plantDto.GardenId, plantDto.SurfaceAreaRequired);
        
        var plantModel = plantMapper.MapToModel(plantDto);
        
        plantRepository.Add(plantModel);
        await plantRepository.SaveChangesAsync();

        var plantId = await GetPlantIdByNameAndGardenId(plantDto.PlantName, plantDto.GardenId);
        plantDto.PlantId = plantId;
        plantDto.RealtimePlantMetricData.PlantId = plantId;
        await rpmdService.AddRealtimePlantMetricData(plantDto.RealtimePlantMetricData);
        
        logger.LogInformation("Plant {plantName} added to garden {gardenId}", plantDto.PlantName, plantDto.GardenId);
    }

    public async Task<PlantDto[]> GetAllPlants()
    {
        var plants = await plantRepository.GetList(p => true);
        
        return plants.Select(plantMapper.MapToDto).ToArray();
    }

    public async Task<PlantDto> GetPlantById(Guid plantId)
    {
        var plant = await plantRepository.Get(p => p.PlantId == plantId);
        if (plant == null) 
            throw new NullReferenceException($"Plant with id {plantId} not found");
        var realtimePlantMetricData = await rpmdService.GetRealtimePlantMetricDataByPlantId(plantId);
        
        var plantDto = plantMapper.MapToDto(plant);
        plantDto.RealtimePlantMetricData = realtimePlantMetricData;
        
        return plantDto;
    }

    private async Task<Guid> GetPlantIdByNameAndGardenId(string plantName, Guid gardenId)
    {
        var plant = await plantRepository.Get(p => p.PlantName == plantName && p.GardenId == gardenId);
        if (plant == null) 
            throw new NullReferenceException($"Added plant with name {plantName} not found in garden {gardenId}");
        return plant.PlantId;
    }

    private async Task ThrowIfNotEnoughFreeSurfaceAreaInGarden(Guid gardenId, double surfaceAreaRequired)
    {
        var freeSurfaceAreaInGarden = await gardenService.GetFreeSurfaceAreaOfGardenWithId(gardenId);
        
        if (freeSurfaceAreaInGarden < surfaceAreaRequired)
            throw new InvalidOperationException($"Not enough free surface area in garden {gardenId}. Required: {surfaceAreaRequired}, Available: {freeSurfaceAreaInGarden}");
    }
}