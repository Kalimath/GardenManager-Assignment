namespace InThePocket.Assignment.GardenManager.Application.Tests.Services.GardenServiceTests;

public interface IRepository<T> where T : class
{
    Task Add(T entity);

    Task<int> SaveChangesAsync();
}