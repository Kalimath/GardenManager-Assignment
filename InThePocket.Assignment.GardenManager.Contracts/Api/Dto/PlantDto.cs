namespace InThePocket.Assignment.GardenManager.Contracts.Api.Dto;

public class PlantDto
{
    public Guid PlantId { get; set; }
    public string PlantName { get; set; }
    public string Species { get; set; }
    public PlantType PlantType { get; set; }
    public DateTime PlantationDate { get; set; }
    public double SurfaceAreaRequired { get; set; }
    public int IdealHumidityLevel { get; set; }
    public RealtimePlantMetricDataDto RealtimePlantMetricData { get; set; }
    public Guid GardenId { get; set; }
}