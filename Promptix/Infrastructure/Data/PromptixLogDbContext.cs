using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class PromptixLogDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=.\\SQLEXPRESS;Database=PromptixLogDB;Trusted_Connection=True;TrustServerCertificate=true"
        );
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditLogType>().HasData(
        new AuditLogType { Id = 1,LogTypeName = "Success",IsActive = true,CreatedDate = DateTime.UtcNow},
        new AuditLogType { Id = 2,LogTypeName = "Warning",IsActive = true,CreatedDate = DateTime.UtcNow},
        new AuditLogType { Id = 3,LogTypeName = "Error",IsActive = true,CreatedDate = DateTime.UtcNow},
        new AuditLogType { Id = 4,LogTypeName = "Information",IsActive = true,CreatedDate = DateTime.UtcNow},
        new AuditLogType { Id = 5,LogTypeName = "NotFound",IsActive = true,CreatedDate = DateTime.UtcNow},
        new AuditLogType { Id = 6,LogTypeName = "NonValidation",IsActive = true,CreatedDate = DateTime.UtcNow}
        );
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        //ChangeTracker.Entries<BaseEntity>() : Follows Entity
        var entries = ChangeTracker.Entries<BaseEntity>();
        foreach (var e in entries)
        {
            if (e.State == EntityState.Added)
            {
                e.Entity.CreatedDate = DateTime.UtcNow;
                e.Entity.IsActive = true;
            }

            if (e.State == EntityState.Modified)
            {
                e.Entity.UpdatedDate = DateTime.UtcNow;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
    public DbSet<AuditLog> AuditLogs { get; set; }
    public DbSet<AuditLogType> AuditLogTypes { get; set; }
}