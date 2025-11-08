using System.Linq.Expressions;

namespace Domain.Interfaces;

public interface IRepository<T> where T : class, IBaseEntity, new()
{
    // Repository Design Pattern:
    // Defining the methods of general operations to be used in tables within databases in a generic way,
    // that is, in a type-independent manner,
    // and creating methods that can be used for all entities as well as all tables. 
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T,bool>> predicate);
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    Task Update(T entity);
    Task Remove(T entity);
    Task RemoveRangeAsync(IEnumerable<T> entities);
}