using InThePocket.Assignment.GardenManager.Application.Domain.Models;
using InThePocket.Assignment.GardenManager.Application.Mappers.Plant;
using InThePocket.Assignment.GardenManager.Application.Shared;
using InThePocket.Assignment.GardenManager.Contracts.Api.Dto;

namespace InThePocket.Assignment.GardenManager.Application.Services.Plant;

public class RpmdService(
    IRepository<RealtimePlantMetricData> rpmdRepository, 
    IRealtimePlantMetricDataMapper mapper) : IRealtimePlantMetricDataService
{
    public async Task AddRealtimePlantMetricData(RealtimePlantMetricDataDto rpmdDto)
    {
        var rpmd = mapper.MapToModel(rpmdDto);
        
        rpmdRepository.Add(rpmd);
        await rpmdRepository.SaveChangesAsync();
    }

    public async Task<RealtimePlantMetricDataDto> GetRealtimePlantMetricDataByPlantId(Guid plantId)
    {
        var rpmd = await rpmdRepository.Get(r => r.PlantId == plantId) 
                   ?? throw new NullReferenceException("The requested Realtime Plant Metric Data can not be found");
        
        return mapper.MapToDto(rpmd);
    }

    public async Task UpdateRealtimePlantMetricData(RealtimePlantMetricDataDto rpmdDto)
    {
        var rpmd = mapper.MapToModel(rpmdDto);
        
        rpmdRepository.Update(rpmd);
        await rpmdRepository.SaveChangesAsync();
    }
}