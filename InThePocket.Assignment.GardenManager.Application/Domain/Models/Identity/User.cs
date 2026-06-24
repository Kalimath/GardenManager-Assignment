using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace InThePocket.Assignment.GardenManager.Application.Domain.Models.Identity;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public short Age { get; init; }
    //Email in base class
    [NotMapped]
    public string Rol { get; set; }
    public ICollection<Garden> Gardens { get; init; } = new HashSet<Garden>();
}