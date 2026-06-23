using InThePocket.Assignment.GardenManager.Contracts.Api.Dto;

namespace InThePocket.Assignment.GardenManager.Application.Mappers.Garden;

public interface IGardenMapper
{
    Models.Garden MapToModel(GardenDto gardenDto);
    GardenDto MapToDto(Models.Garden gardenModel);
}