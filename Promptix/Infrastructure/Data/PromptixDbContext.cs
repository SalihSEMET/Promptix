using System.Linq.Expressions;
using System.Reflection;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class PromptixDbContext : IdentityDbContext<AppUser,AppRole,int>
{
    //Our Working Method While Performing Configuration Operations
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=.\\SQLEXPRESS;Database=PromptixDB;Trusted_Connection=True;TrustServerCertificate=true"
            );
        base.OnConfiguring(optionsBuilder);
    }
    //Our structure connects to the database from within the application and runs the models as tables.
    protected override void OnModelCreating(ModelBuilder builder)
    {
        //It finds all the configurations we defined in the configuration folder and applies them one by one.
        var configuration = Assembly.GetExecutingAssembly();
        builder.ApplyConfigurationsFromAssembly(configuration);
        //Returns All Entities Inheriting from the Base Entity
        foreach (var item in builder.Model.GetEntityTypes().Where(t=>typeof(Domain.Entities.BaseEntity).IsAssignableFrom(t.ClrType)))
        {
            //When retrieving data from the database, it only retrieves data whose IsActive column is 1 and true.
            var parameters = Expression.Parameter(item.ClrType, "e");
            var prop = Expression.Property(parameters, nameof(BaseEntity.IsActive));
            var condition = Expression.Equal(prop, Expression.Constant(true));
            var lambda = Expression.Lambda(condition, parameters);
            builder.Entity(item.ClrType).HasQueryFilter(lambda);
            //var prompt = await __dbContext.Prompts.ToListAsync() == Select * from where IsActive = 1
        }
        //First, the indexing process was done according to two columns, that is, the sorting process was done, and then both columns were defined as Unique. That is, the user cannot add the same prompt to their favorites twice.
        builder.Entity<Favorite>().HasIndex(f => new
        {
            f.AppUserId,
            f.PromptId
        }).IsUnique();
        builder.Entity<Purchase>().HasIndex(x => new
        {
            x.AppUserId,
            x.PromptId,
            x.PurchaseDate
        });
        base.OnModelCreating(builder);
    }
    //When a new entity is added, we set the date to the current date. If the entity is updated, we update the update date to the current date.
    //By overriding the Save Changes Method, we enter the save queue and perform the operations we want to do. If new data is to be added according to our controls, we set the insertion date to today's date and change the value of the IsActive Column to true. If the data is to be changed, i.e. updated, we set the updateDate value to the time of day.
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        //ChangeTracker.Entries<BaseEntity>() : Follows Entity
        var entries = ChangeTracker.Entries<BaseEntity>();
        foreach (var item in entries)
        {
            if (item.State == EntityState.Added)
            {
                item.Entity.CreatedDate = DateTime.Now;
                item.Entity.IsActive = true;
            }
            if (item.State == EntityState.Modified)
                item.Entity.UpdatedDate = DateTime.Now;
        }
        return base.SaveChangesAsync(cancellationToken);
    }

    //DBSet = Table
    public DbSet<AuditLog> AuditLogs{ get; set; }
    public DbSet<Category> Categories{ get; set; }
    public DbSet<Favorite> Favorites{ get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Prompt> Prompts { get; set; }
    public DbSet<PromptCategory> PromptCategories { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
}