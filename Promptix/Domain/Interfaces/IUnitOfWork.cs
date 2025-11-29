using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Prompt> Prompts { get; }
        IRepository<Category> Categories { get; }
        IRepository<Favorite> Favorites { get; }
        IRepository<Payment> Payments { get; }
        IRepository<AuditLog> AuditLogs { get; }
        IRepository<Purchase> Purchases { get;}
        IRepository<Subscription> Subscriptions { get; }
        IRepository<PromptCategory> PromptCategories { get; }
        Task<int> CompleteAsync(bool transactionActive = true);
        Task BeginTransactionAsync();
        Task<int> CommitTransactionAsync();
        Task RollbackTransactionAsync();

    }
}
