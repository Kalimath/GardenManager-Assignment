using InThePocket.Assignment.GardenManager.Application.Models;
using InThePocket.Assignment.GardenManager.Application.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InThePocket.Assignment.GardenManager.Ports.Database;

public class GardenManagerContext : IdentityDbContext<User, Role, Guid>
{
    public GardenManagerContext(DbContextOptions<GardenManagerContext> options) : base(options)
    {

    }
    
    public DbSet<Garden> Gardens { get; set; } = null!;
}