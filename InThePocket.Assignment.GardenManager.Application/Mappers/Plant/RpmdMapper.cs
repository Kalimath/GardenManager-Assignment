using InThePocket.Assignment.GardenManager.Application.Domain.Models;
using InThePocket.Assignment.GardenManager.Contracts.Api.Dto;

namespace InThePocket.Assignment.GardenManager.Application.Mappers.Plant;

public class RpmdMapper : IRealtimePlantMetricDataMapper
{
    public RealtimePlantMetricData MapToModel(RealtimePlantMetricDataDto rpmdDto)
    {
        return new RealtimePlantMetricData()
        {
            RealtimePlantMetricDataId = rpmdDto.RealtimePlantMetricDataId,
            CurrentHumidityLevel = rpmdDto.CurrentHumidityLevel,
            LastIrrigationStartTime = rpmdDto.LastIrrigationStartTime,
            LastIrrigationEndTime = rpmdDto.LastIrrigationEndTime,
            PlantId = rpmdDto.PlantId
        };
    }

    public RealtimePlantMetricDataDto MapToDto(RealtimePlantMetricData rpmdModel)
    {
        throw new NotImplementedException();
    }
}