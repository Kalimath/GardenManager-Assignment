namespace InThePocket.Assignment.GardenManager.Application.Models;

public class RealtimePlantMetricDataDto
{
    public double CurrentHumidityLevel { get; set; }
    public DateTime LastIrrigationStartTime { get; set; }
    public DateTime LastIrrigationEndTime { get; set; }
    public Guid PlantId { get; set; }
}