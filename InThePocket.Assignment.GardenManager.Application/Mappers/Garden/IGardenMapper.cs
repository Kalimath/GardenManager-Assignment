using InThePocket.Assignment.GardenManager.Contracts.Dto;

namespace InThePocket.Assignment.GardenManager.Application.Mappers.Garden;

public interface IGardenMapper
{
    Models.Garden MapToModel(GardenDto gardenDto);
}