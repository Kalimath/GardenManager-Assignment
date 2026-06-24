using InThePocket.Assignment.GardenManager.Contracts.Api.Dto;

namespace InThePocket.Assignment.GardenManager.Application.Mappers.Plant;

public class PlantMapper : IPlantMapper
{
    public Domain.Models.Plant MapToModel(PlantDto plantDto)
    {
        return new Domain.Models.Plant
        {
            PlantId = plantDto.PlantId,
            PlantName = plantDto.PlantName,
            Species = plantDto.Species,
            PlantType = plantDto.PlantType,
            PlantationDate = plantDto.PlantationDate,
            SurfaceAreaRequired = plantDto.SurfaceAreaRequired,
            IdealHumidityLevel = plantDto.IdealHumidityLevel,
            GardenId = plantDto.GardenId
        };
    }

    public PlantDto MapToDto(Domain.Models.Plant plantModel)
    {
        return new PlantDto
        {
            PlantId = plantModel.PlantId,
            PlantName = plantModel.PlantName,
            Species = plantModel.Species,
            PlantType = plantModel.PlantType,
            PlantationDate = plantModel.PlantationDate,
            SurfaceAreaRequired = plantModel.SurfaceAreaRequired,
            IdealHumidityLevel = plantModel.IdealHumidityLevel,
            GardenId = plantModel.GardenId
        };
    }
    
    
}