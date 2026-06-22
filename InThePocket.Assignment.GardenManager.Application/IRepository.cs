namespace InThePocket.Assignment.GardenManager.Application;

public interface IRepository<T> where T : class
{
    Task Add(T entity);

    Task<int> SaveChangesAsync();
}