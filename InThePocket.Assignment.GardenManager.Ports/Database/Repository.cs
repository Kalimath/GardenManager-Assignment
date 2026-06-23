using System.Linq.Expressions;
using InThePocket.Assignment.GardenManager.Application.Shared;
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

    public Task<T?> Get(Expression<Func<T, bool>> predicate)
    {
        return _modelDbSets.Where(predicate).FirstOrDefaultAsync();
    }

    public Task<int> SaveChangesAsync()
    {
        return gardenManagerContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> GetList(Expression<Func<T, bool>> predicate)
    {
        return await _modelDbSets.Where(predicate).ToListAsync<T>();
    }
}