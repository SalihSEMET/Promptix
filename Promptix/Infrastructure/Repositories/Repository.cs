using System.Linq.Expressions;
using Domain.Enums;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class,IBaseEntity, new()
{
    protected readonly PromptixDbContext DbContext;
    protected readonly DbSet<T> DbSet;

    public Repository(PromptixDbContext context)
    {
        DbContext = context;
        DbSet = context.Set<T>();
    }
    public async Task<T?> GetByIdAsync(int id) => await DbSet.FindAsync(id);
    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T,bool>> filter = null ,OrderType orderBy = OrderType.Asc,params string[] include)
    {
        IQueryable<T> query = DbSet.AsQueryable();
        if (filter != null)
            query = query.Where(filter);
        foreach (var item in include)
        {
            query = query.Include(item);
        }
        
        query = orderBy == OrderType.Asc ? query.OrderBy(x => x.Id) : query.OrderByDescending(x => x.Id);
        
        return await DbSet.ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate,OrderType orderBy = OrderType.Asc,params string[] include)
    {
        IQueryable<T> query = DbSet.AsQueryable(); //In Query Status: Our query has not yet been sent to the database. Used during the query preparation phase.
        query = query.Where(predicate);
        foreach (var item in include)
        {
            query = query.Include(item);
        }
        query = orderBy == OrderType.Asc ? query.OrderBy(x => x.Id) : query.OrderByDescending(x => x.Id);
        
        return await DbSet.ToListAsync(); //The query is sent to the database, executed, and the results are returned.
    }
    public async Task AddAsync(T entity) => await DbSet.AddAsync(entity);
    public async Task AddRangeAsync(IEnumerable<T> entities) => await DbSet.AddRangeAsync(entities);
    public void Update(T entity) => DbSet.Update(entity);
    public void Remove(T entity) =>  DbSet.Remove(entity);
    public void RemoveRangeAsync(IEnumerable<T> entities) => DbSet.RemoveRange(entities);
    // select * from T
    public IQueryable<T> Queryable() => DbSet.AsQueryable();
}