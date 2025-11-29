using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly PromptixDbContext dbContext;
        private readonly PromptixLogDbContext logDbContext;
        private IDbContextTransaction? transaction;

        public IRepository<Prompt> prompts;

        public IRepository<Category> categories;

        public IRepository<Favorite> favorites;

        public IRepository<Payment> payments;
        public IRepository<AuditLog> auditLogs;

        public IRepository<Purchase> purchases;

        public IRepository<Subscription> subscriptions;


        public IRepository<PromptCategory> promptCategories;

        public UnitOfWork(PromptixDbContext context, PromptixLogDbContext logDbContext)
        {
            dbContext = context;
            this.logDbContext = logDbContext;
        }


        public IRepository<Prompt> Prompts => prompts ??= new Repository<Prompt,PromptixDbContext>(dbContext);

        public IRepository<Category> Categories => categories ??= new Repository<Category, PromptixDbContext>(dbContext);

        public IRepository<Favorite> Favorites => favorites ??= new Repository<Favorite, PromptixDbContext>(dbContext);
        public IRepository<AuditLog> AuditLogs => auditLogs ??= new Repository<AuditLog, PromptixLogDbContext>(logDbContext);


        public IRepository<Payment> Payments => payments ??= new Repository<Payment, PromptixDbContext>(dbContext);

        public IRepository<Purchase> Purchases => purchases ??= new Repository<Purchase, PromptixDbContext>(dbContext);

        public IRepository<Subscription> Subscriptions => subscriptions ??= new Repository<Subscription, PromptixDbContext>(dbContext);

        public IRepository<PromptCategory> PromptCategories => promptCategories ??= new Repository<PromptCategory,PromptixDbContext>(dbContext);

        public async Task BeginTransactionAsync() => transaction = await dbContext.Database.BeginTransactionAsync();

        public async Task<int> CommitTransactionAsync()
        {
            if (transaction == null) return 0;

            int result = await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
            await transaction.DisposeAsync();
            transaction = null;
            return result;
        }

        //public async Task<int> CompleteAsync() => await dbContext.SaveChangesAsync();


        public async Task<int> CompleteAsync(bool transactionActive = true)
        {
            try
            {
                if (transactionActive)
                {
                    await BeginTransactionAsync();
                    return await CommitTransactionAsync();
                    
                }
                else
                {
                    return await dbContext.SaveChangesAsync();
                }


            }
            catch(Exception ex)
            {
                await RollbackTransactionAsync();
                throw ex;
            }


        } 

        public async Task RollbackTransactionAsync()
        {
            if (transaction == null) return;

            await transaction.RollbackAsync();
            await transaction.DisposeAsync();
            transaction = null;
        }

        public void Dispose()
        {
            transaction?.Dispose();
            dbContext.Dispose();
        }
    }
}
