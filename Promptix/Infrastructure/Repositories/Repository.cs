using Domain.Enums;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class Repository<T,TDBContext> : IRepository<T>
        where T : class, IBaseEntity, new()
        where TDBContext : DbContext
    {
        protected readonly TDBContext dbContext;
        protected readonly DbSet<T> dbSet;

        public Repository(TDBContext context)
        {
            dbContext = context;
            dbSet = context.Set<T>();
        }
        public async Task AddAsync(T entity) => await dbSet.AddAsync(entity);

        public async Task AddRangeAsync(IEnumerable<T> entities) => await dbSet.AddRangeAsync(entities);

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, OrderType orderBy = OrderType.ASC, params string[] include)
        {
            IQueryable<T> query = dbSet.AsQueryable(); // Sorgu halinde henüz veritabanına sorgumuz gönderilmedi. Sorgu hazırlanma aşamasında kullanılır.

            query = query.Where(predicate);

            foreach (var item in include)
            {
                query = query.Include(item);
            }

            query = orderBy == OrderType.ASC ? query.OrderBy(x => x.Id) : query.OrderByDescending(x => x.Id);

            return await query.ToListAsync(); // sorgu veritabanına göndertilir çalıştırılır ve sonuçlar döndürülür.

        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, OrderType orderBy = OrderType.ASC, params string[] include)
        {
            IQueryable<T> query = dbSet.AsQueryable();

            if(filter != null)
                query = query.Where(filter);

            foreach (var item in include)
            {
                query = query.Include(item);
            }

            
            query = orderBy==OrderType.ASC ? query.OrderBy(x=>x.Id) : query.OrderByDescending(x=>x.Id);


            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id) => await dbSet.FindAsync(id);

        public void Remove(T entity) => dbSet.Remove(entity);

        public void RemoveRangeAsync(IEnumerable<T> entities) => dbSet.RemoveRange(entities);

        public void Update(T entity) => dbSet.Update(entity);

        //select * from T 
        public IQueryable<T> Query() => dbSet.AsQueryable();
    }
}
