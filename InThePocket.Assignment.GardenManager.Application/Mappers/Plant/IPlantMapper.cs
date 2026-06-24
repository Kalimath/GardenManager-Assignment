using InThePocket.Assignment.GardenManager.Contracts.Api.Dto;

namespace InThePocket.Assignment.GardenManager.Application.Mappers.Plant;

public interface IPlantMapper
{
    Domain.Models.Plant MapToModel(PlantDto plantDto);
    PlantDto MapToDto(Domain.Models.Plant plantModel);
}