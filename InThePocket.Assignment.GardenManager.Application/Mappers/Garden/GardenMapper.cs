using InThePocket.Assignment.GardenManager.Application.Models.Identity;
using InThePocket.Assignment.GardenManager.Contracts.Api.Dto;

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
            UserId = gardenDto.UserId
        };
    }

    public GardenDto MapToDto(Models.Garden gardenModel)
    {
        return new GardenDto
        {
            GardenId = gardenModel.GardenId,
            GardenName = gardenModel.GardenName,
            TotalSurfaceArea = gardenModel.TotalSurfaceArea,
            LocationDescription = gardenModel.LocationDescription,
            TargetHumidityLevel = gardenModel.TargetHumidityLevel,
            UserId = gardenModel.UserId
        };
    }
}