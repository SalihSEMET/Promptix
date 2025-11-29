using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class PromptixLogDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=PromptixLogDB;Trusted_Connection=True;TrustServerCertificate=true");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditLogType>().HasData(
                new AuditLogType { Id = 1, LogTypeName = "Success",IsActive=true,CreatedDate=DateTime.Now },
                new AuditLogType { Id = 2, LogTypeName = "Warning", IsActive = true, CreatedDate = DateTime.Now },
                new AuditLogType { Id = 3, LogTypeName = "Error", IsActive = true, CreatedDate = DateTime.Now },
                new AuditLogType { Id = 4, LogTypeName = "Information", IsActive = true, CreatedDate = DateTime.Now },
                new AuditLogType { Id = 5, LogTypeName = "NotFound", IsActive = true, CreatedDate = DateTime.Now },
                new AuditLogType { Id = 6, LogTypeName = "NonValidation", IsActive = true, CreatedDate = DateTime.Now }
            );
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // ChangeTracker.Entries<BaseEntity>() : Entity yi takip eder.
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var e in entries)
            {
                if (e.State == EntityState.Added)
                {
                    e.Entity.CreatedDate = DateTime.Now;
                    e.Entity.IsActive = true;
                }

                if (e.State == EntityState.Modified)
                    e.Entity.UpdatedDate = DateTime.Now;

            }
            return base.SaveChangesAsync(cancellationToken);
        }


        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<AuditLogType> AuditLogTypes { get; set; }

    }
}
