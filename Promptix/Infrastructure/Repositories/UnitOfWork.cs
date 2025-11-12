using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly PromptixDbContext DbContext;
    private IDbContextTransaction? Transaction;
    public IRepository<Prompt> Prompts { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public IRepository<Category> Categories { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public IRepository<Favorite> Favorites { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public IRepository<Payment> Payments { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public IRepository<Purchase> Purchases { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public IRepository<Subscription> Subscriptions { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public IRepository<AuditLog> AuditLogs { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public IRepository<PromptCategory> PromptCategories { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public Task<int> CompleteAsync()
    {
        throw new NotImplementedException();
    }

    public Task BeginTransactionAsync()
    {
        throw new NotImplementedException();
    }

    public Task CommitTransactionAsync()
    {
        throw new NotImplementedException();
    }

    public Task RollbackTransactionAsync()
    {
        throw new NotImplementedException();
    }
}