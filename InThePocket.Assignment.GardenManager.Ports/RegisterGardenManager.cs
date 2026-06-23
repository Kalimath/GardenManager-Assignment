using InThePocket.Assignment.GardenManager.Application.Mappers.Garden;
using InThePocket.Assignment.GardenManager.Application.Services.Garden;
using InThePocket.Assignment.GardenManager.Application.Shared;
using InThePocket.Assignment.GardenManager.Ports.Database;

namespace InThePocket.Assignment.GardenManager.Ports;

public static class RegisterGardenManager
{
    public static void RegisterDependencies(this IServiceCollection services)
    {
        services.AddScoped<IGardenService, GardenService>();
        services.AddScoped<IGardenMapper, GardenMapper>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }
}