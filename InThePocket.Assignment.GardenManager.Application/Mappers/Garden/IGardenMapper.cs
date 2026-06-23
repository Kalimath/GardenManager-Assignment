using InThePocket.Assignment.GardenManager.Application.Models.Identity;
using InThePocket.Assignment.GardenManager.Contracts.Api.Dto;

namespace InThePocket.Assignment.GardenManager.Application.Mappers.Garden;

public interface IGardenMapper
{
    Models.Garden MapToModel(GardenDto gardenDto, User user);
    GardenDto MapToDto(Models.Garden gardenModel);
}