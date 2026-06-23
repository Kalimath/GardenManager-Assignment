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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //admin user
        modelBuilder.Entity<User>().HasData([new User
        {
            Id = Guid.Parse("b3c1a8f4-5d6e-4a7b-9c2e-8f3d7a6e1c4f"),
            FirstName = "John",
            LastName = "Doe",
            Age = 34,
            Email = "john.doe@email.com"
        }]);

        base.OnModelCreating(modelBuilder);
    }
    
    public DbSet<Garden> Gardens { get; set; } = null!;
}