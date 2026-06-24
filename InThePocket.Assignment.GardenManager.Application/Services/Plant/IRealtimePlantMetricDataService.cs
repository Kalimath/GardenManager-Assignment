using InThePocket.Assignment.GardenManager.Contracts.Api.Dto;

namespace InThePocket.Assignment.GardenManager.Application.Services.Plant;

public interface IRealtimePlantMetricDataService
{
    Task AddRealtimePlantMetricData(RealtimePlantMetricDataDto rpmdDto);
}