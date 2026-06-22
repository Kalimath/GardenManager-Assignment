namespace InThePocket.Assignment.GardenManager.Application;

public interface IRepository<T> where T : class
{
    void Add(T entity);

    Task<int> SaveChangesAsync();
}