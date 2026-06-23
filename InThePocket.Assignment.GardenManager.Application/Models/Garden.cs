using InThePocket.Assignment.GardenManager.Application.Models.Identity;

namespace InThePocket.Assignment.GardenManager.Application.Models;

public class Garden
{
    public Guid GardenId { get; init; }
    public string GardenName { get; init; }
    public double TotalSurfaceArea { get; init; }
    public string LocationDescription { get; init; }
    public int TargetHumidityLevel { get; init; }
    public Guid UserId { get; init; }
    public User User { get; init; }
}