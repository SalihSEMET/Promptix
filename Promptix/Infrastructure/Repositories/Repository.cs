using System.Linq.Expressions;
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
    public async Task<IEnumerable<T>> GetAllAsync() => await DbSet.ToListAsync();
    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate) =>
        await DbSet.Where(predicate).ToListAsync();
    public async Task AddAsync(T entity) => await DbSet.AddAsync(entity);
    public async Task AddRangeAsync(IEnumerable<T> entities) => await DbSet.AddRangeAsync(entities);
    public void Update(T entity) => DbSet.Update(entity);
    public void Remove(T entity) =>  DbSet.Remove(entity);
    public void RemoveRangeAsync(IEnumerable<T> entities) => DbSet.RemoveRange(entities);
    // select * from T
    public IQueryable<T> Queryable() => DbSet.AsQueryable();
}