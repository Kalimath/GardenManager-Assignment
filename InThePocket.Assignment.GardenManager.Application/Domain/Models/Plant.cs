using InThePocket.Assignment.GardenManager.Contracts.Api;

namespace InThePocket.Assignment.GardenManager.Application.Domain.Models;

public class Plant
{
    public Guid PlantId { get; init; }
    public string PlantName { get; init; }
    public string Species { get; init; }
    public PlantType PlantType { get; init; }
    public DateTime PlantationDate { get; init; }
    public double SurfaceAreaRequired { get; init; }
    public int IdealHumidityLevel { get; init; }
    public Guid GardenId { get; init; }
    
    public RealtimePlantMetricData? RealtimePlantMetricData { get; init; }
    
}