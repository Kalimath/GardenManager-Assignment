namespace InThePocket.Assignment.GardenManager.Application.Domain.Models;

public class RealtimePlantMetricData
{
    public Guid RealtimePlantMetricDataId { get; init; }
    public double CurrentHumidityLevel { get; init; }
    public DateTime LastIrrigationStartTime { get; init; }
    public DateTime LastIrrigationEndTime { get; init; }
    public Guid PlantId { get; set; }
    public virtual Plant Plant { get; set; }
}