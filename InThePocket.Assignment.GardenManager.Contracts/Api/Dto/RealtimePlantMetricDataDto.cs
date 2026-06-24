namespace InThePocket.Assignment.GardenManager.Contracts.Api.Dto;

public class RealtimePlantMetricDataDto
{
    public Guid RealtimePlantMetricDataId { get; set; }
    public double CurrentHumidityLevel { get; set; }
    public DateTime LastIrrigationStartTime { get; set; }
    public DateTime LastIrrigationEndTime { get; set; }
    public Guid PlantId { get; set; }
}