using Microsoft.AspNetCore.Identity;

namespace InThePocket.Assignment.GardenManager.Application.Models.Identity;

public class Role : IdentityRole<Guid>
{
    public Role()
    {
    }

    public Role(string roleName) : base(roleName)
    {
    }
}