using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork,IDisposable
{
    private readonly PromptixDbContext _dbContext;
    private IDbContextTransaction? _transaction;
    public IRepository<Prompt> prompts;
    public IRepository<Category> categories;
    public IRepository<Favorite> favorites;
    public IRepository<Payment> payments;
    public IRepository<Purchase> purchases;
    public IRepository<Subscription> subscriptions;
    public IRepository<AuditLog> auditLogs;
    public IRepository<PromptCategory> promptCategories;
    public UnitOfWork(PromptixDbContext context)
    {
        _dbContext = context;
    }
    public IRepository<Prompt> Prompts => prompts;
    public IRepository<Category> Categories => categories;
    public IRepository<Favorite> Favorites => favorites;
    public IRepository<Payment> Payments => payments;
    public IRepository<Purchase> Purchases => purchases;
    public IRepository<Subscription> Subscriptions => subscriptions;
    public IRepository<AuditLog> AuditLogs => auditLogs;
    public IRepository<PromptCategory> PromptCategories => promptCategories;
    public async Task<int> CompleteAsync() => await _dbContext.SaveChangesAsync();
    public async Task BeginTransactionAsync() => _transaction = await _dbContext.Database.BeginTransactionAsync();
    public async Task CommitTransactionAsync()
    {
        if (_transaction == null) return;
        await _dbContext.SaveChangesAsync();
        await _transaction.CommitAsync();
        await _transaction.DisposeAsync();
        _transaction = null;
    }
    public async Task RollbackTransactionAsync()
    {
        if (_transaction == null) return;
        await _transaction.RollbackAsync();
        await _transaction.DisposeAsync();
        _transaction = null;
    }
    public void Dispose()
    {
        _transaction?.Dispose();
        _dbContext.Dispose();
    }
}