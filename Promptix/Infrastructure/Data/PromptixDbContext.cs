using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class PromptixDbContext : IdentityDbContext<AppUser,AppRole,int>
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=SQLEXPRESS;Database=PromptixDB;Trusted_Connection=True;"
            );
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        
        base.OnModelCreating(builder);
    }
    public DbSet<AuditLog> AuditLogs{ get; set; }
    public DbSet<Category> Categories{ get; set; }
    public DbSet<Favorite> Favorites{ get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Prompt> Prompts { get; set; }
    public DbSet<PromptCategory> PromptCategories { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
}