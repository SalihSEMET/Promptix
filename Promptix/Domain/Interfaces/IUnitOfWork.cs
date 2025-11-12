using Domain.Entities;

namespace Domain.Interfaces;

public interface IUnitOfWork
{
    public IRepository<Prompt> Prompts { get;}
    public IRepository<Category> Categories { get;}
    public IRepository<Favorite> Favorites { get;}
    public IRepository<Payment> Payments { get;}
    public IRepository<Purchase> Purchases { get;}
    public IRepository<Subscription> Subscriptions { get;}
    public IRepository<AuditLog> AuditLogs { get;}
    public IRepository<PromptCategory> PromptCategories { get;}
    Task<int> CompleteAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}