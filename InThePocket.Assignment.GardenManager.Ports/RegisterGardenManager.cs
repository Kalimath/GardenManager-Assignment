using InThePocket.Assignment.GardenManager.Application.Services.Garden;

namespace InThePocket.Assignment.GardenManager.Ports;

public static class RegisterGardenManager
{
    public static void RegisterDependencies(this IServiceCollection services)
    {
        services.AddScoped<IGardenService, GardenService>();
    }
}