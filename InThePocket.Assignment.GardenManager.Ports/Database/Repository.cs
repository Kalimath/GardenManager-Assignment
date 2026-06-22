using InThePocket.Assignment.GardenManager.Application;
using Microsoft.EntityFrameworkCore;

namespace InThePocket.Assignment.GardenManager.Ports.Database;

public class Repository<T>(GardenManagerContext gardenManagerContext) : IRepository<T>
    where T : class
{
    private readonly DbSet<T> _modelDbSets = gardenManagerContext.Set<T>();

    public void Add(T entity)
    {
        _modelDbSets.Add(entity);
    }

    public Task<int> SaveChangesAsync()
    {
        return gardenManagerContext.SaveChangesAsync();
    }
}