using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<T> where T : class, IBaseEntity, new()
    {
        // Repository Design Pattern: Veritabannları içerisindeki tablolarda kullanılacak olan genel işlemlerin methodlarının Generic bir şekilde yani tip bağımsız bir şekilde tanımlanıp bütün entityler yani bütün tablolar için kullanılabilecek şekilde methodların oluşturulması.

        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, OrderType orderBy = OrderType.ASC, params string[] include); 
        Task<IEnumerable<T>> FindAsync(Expression<Func<T,bool>> predicate, OrderType orderBy = OrderType.ASC, params string[] include);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRangeAsync(IEnumerable<T> entities);
        

    }
}
