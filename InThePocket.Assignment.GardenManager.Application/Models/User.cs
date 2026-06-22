namespace InThePocket.Assignment.GardenManager.Application.Models;

public class User
{
    public Guid UserId { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public DateOnly BirthDate { get; init; }
    public string Email { get; init; }
    public ICollection<Garden> Gardens { get; init; } = new List<Garden>();
}