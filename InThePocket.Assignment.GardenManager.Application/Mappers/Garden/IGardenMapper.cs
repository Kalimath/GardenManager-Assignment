using InThePocket.Assignment.GardenManager.Contracts.Api.Dto;

namespace InThePocket.Assignment.GardenManager.Application.Mappers.Garden;

public interface IGardenMapper
{
    Domain.Models.Garden MapToModel(GardenDto gardenDto);
    GardenDto MapToDto(Domain.Models.Garden gardenModel);
}