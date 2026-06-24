using InThePocket.Assignment.GardenManager.Application.Domain.Models;
using InThePocket.Assignment.GardenManager.Contracts.Api.Dto;

namespace InThePocket.Assignment.GardenManager.Application.Mappers.Plant;

public interface IRealtimePlantMetricDataMapper
{
    RealtimePlantMetricData MapToModel(RealtimePlantMetricDataDto rpmdDto);
    RealtimePlantMetricDataDto MapToDto(RealtimePlantMetricData rpmdModel);
}