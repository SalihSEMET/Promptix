using System.Linq.Expressions;
using Domain.Enums;

namespace Domain.Interfaces;

public interface IRepository<T> where T : class, IBaseEntity, new()
{
    // Repository Design Pattern:
    // Defining the methods of general operations to be used in tables within databases in a generic way,
    // that is, in a type-independent manner,
    // and creating methods that can be used for all entities as well as all tables. 
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T,bool>> filter = null ,OrderType orderBy = OrderType.Asc,params string[] include);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T,bool>> predicate,OrderType orderBy = OrderType.Asc,params string[] include);
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    void Update(T entity);
    void Remove(T entity);
    void RemoveRangeAsync(IEnumerable<T> entities);
}