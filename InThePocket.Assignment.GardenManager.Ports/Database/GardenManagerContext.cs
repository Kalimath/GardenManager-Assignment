using InThePocket.Assignment.GardenManager.Application.Domain.Models;
using InThePocket.Assignment.GardenManager.Application.Domain.Models.Identity;
using InThePocket.Assignment.GardenManager.Contracts.Api;
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
        
        /*modelBuilder.Entity<Plant>()
            .HasOne(e => e.RealtimePlantMetricData)
            .WithOne(e => e.Plant)
            .HasForeignKey<RealtimePlantMetricData>(e => e.PlantId);*/
        
        modelBuilder.Entity<Plant>()
            .HasOne(c => c.RealtimePlantMetricData)
            .WithOne(i => i.Plant)
            .HasForeignKey<RealtimePlantMetricData>(b => b.PlantId);
        
        modelBuilder.Entity<Plant>()
            .Property(p => p.PlantType)
            .HasConversion(
                // To database: enum -> string
                v => v.ToString(),
                // From database: string -> enum
                v => (PlantType)Enum.Parse(typeof(PlantType), v));

        base.OnModelCreating(modelBuilder);
    }
    
    public DbSet<Garden> Gardens { get; set; } = null!;
    public DbSet<Plant> Plants { get; set; } = null!;
    public DbSet<RealtimePlantMetricData> RealtimePlantMetricData { get; set; } = null!;
}