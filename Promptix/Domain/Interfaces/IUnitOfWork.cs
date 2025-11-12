using Domain.Entities;

namespace Domain.Interfaces;

public interface IUnitOfWork
{
    public IRepository<Prompt> Prompts { get; set; }
    public IRepository<Category> Categories { get; set; }
    public IRepository<Favorite> Favorites { get; set; }
    public IRepository<Payment> Payments { get; set; }
    public IRepository<Purchase> Purchases { get; set; }
    public IRepository<Subscription> Subscriptions { get; set; }
    public IRepository<AuditLog> AuditLogs { get; set; }
    public IRepository<PromptCategory> PromptCategories { get; set; }
    Task<int> CompleteAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}