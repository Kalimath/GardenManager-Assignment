using System.Linq.Expressions;

namespace InThePocket.Assignment.GardenManager.Application.Shared;

public interface IRepository<T> where T : class
{
    void Add(T entity);
    Task<T?> Get(Expression<Func<T, bool>> predicate);

    Task<int> SaveChangesAsync();
    Task<IEnumerable<T>> GetList(Expression<Func<T, bool>> predicate);
    Task<bool> Any(Expression<Func<T, bool>> predicate);
    void Update(T updateEntity);
    void Delete(T entity);
}