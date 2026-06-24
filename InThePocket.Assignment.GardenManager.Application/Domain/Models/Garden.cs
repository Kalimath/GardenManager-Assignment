using InThePocket.Assignment.GardenManager.Application.Domain.Models.Identity;

namespace InThePocket.Assignment.GardenManager.Application.Domain.Models;

public class Garden
{
    public Guid GardenId { get; init; }
    public string GardenName { get; init; }
    public double TotalSurfaceArea { get; init; }
    public string LocationDescription { get; init; }
    public int TargetHumidityLevel { get; init; }
    public Guid UserId { get; init; }
    public User User { get; init; }
    public ICollection<Plant> Plants { get; set; } = new HashSet<Plant>();
}