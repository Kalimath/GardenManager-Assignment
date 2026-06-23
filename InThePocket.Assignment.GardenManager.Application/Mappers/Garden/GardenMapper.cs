using InThePocket.Assignment.GardenManager.Application.Models.Identity;
using InThePocket.Assignment.GardenManager.Contracts.Dto;

namespace InThePocket.Assignment.GardenManager.Application.Mappers.Garden;

public class GardenMapper : IGardenMapper
{
    public Models.Garden MapToModel(GardenDto gardenDto, User user)
    {
        return new Models.Garden
        {
            GardenId = gardenDto.GardenId,
            GardenName = gardenDto.GardenName,
            TotalSurfaceArea = gardenDto.TotalSurfaceArea,
            LocationDescription = gardenDto.LocationDescription,
            TargetHumidityLevel = gardenDto.TargetHumidityLevel,
            User = user
        };
    }
}