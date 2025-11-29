using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class PromptixDbContext : IdentityDbContext<AppUser,AppRole,int>
    {
        //Konfigürasyon İşlemleri Yapılırken Çalışan Methodumuz
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=PromptixDB;Trusted_Connection=True;TrustServerCertificate=true");
            base.OnConfiguring(optionsBuilder);
        }

        //Uygulama içerisinden veritabanına bağlanıp, modellerin tablo olarak oluşturulurken çalıştığı yapımız.
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Configurations klasörü içerisindeki bütün tanımladığımız konfigurasyonları bulur ve teker teker uygular.
            var configurationAssembly = Assembly.GetExecutingAssembly();
            builder.ApplyConfigurationsFromAssembly(configurationAssembly);


            //BaseEntity den kalıtım alan bütün Entityleri döner
            foreach (var item in builder.Model.GetEntityTypes().Where(t=> typeof(BaseEntity).IsAssignableFrom(t.ClrType)))
            {

                //Veritabanından veri çekerken default olarak yani varsayılan olarak sadece IsActive kolonu 1 olan yani true olanları yani Aktif olan verileri çeker.
                var parameter = Expression.Parameter(item.ClrType, "e");
                var prop = Expression.Property(parameter, nameof(BaseEntity.IsActive));
                var condition = Expression.Equal(prop, Expression.Constant(true));
                var lambda = Expression.Lambda(condition, parameter);
                builder.Entity(item.ClrType).HasQueryFilter(lambda);

                //ÖRneğin;
                // var prompts = await _dbContext.Prompts.ToListAsync() dediğimizde
                // Select * from Prompts where IsActive = 1 sorgusunu üretir.

            }

            //Önce iki kolona göre indexleme işlemi yaptık yani sıralama işlemi yaptık daha sonrasında ise Unique olarak tanımladık iki kolonu birden. Yani bir kullanıcı aynı promptu bir daha favorilerine ekleyemez.
            builder.Entity<Favorite>().HasIndex(f => new { f.AppUserId,f.PromptId}).IsUnique();

            builder.Entity<Purchase>().HasIndex(x => new { x.AppUserId, x.PromptId, x.PurchaseDate });

            base.OnModelCreating(builder);
        }


        //Yeni bir entity eklenmişse eklenme tarihini o anki tarih yapıyoruz. Eğer Entity güncellenmişse Güncelleme tarihini o anki tarih olarak güncelliyoruz.

        //SaveChanges methodunu override ederek kaydedilme sırasında araya giriyoruz ve yapmak istediğimiz işlemleri yaparak kontrollerimize göre eğer yeni veri ekleniyorsa eklenme tarihini o an ki tarih yapıp IsActive kolonun değerini ise true olarak değiştiriyoruz, eğer veri Modified edilmişse yani güncellenecekse ozaman da UpdatedDate değerini o anki zaman ile güncelliyoruz.
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // ChangeTracker.Entries<BaseEntity>() : Entity yi takip eder.
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var e in entries)
            {
                if(e.State == EntityState.Added)
                {
                    e.Entity.CreatedDate = DateTime.Now;
                    e.Entity.IsActive = true;
                }

                if (e.State == EntityState.Modified)
                    e.Entity.UpdatedDate = DateTime.Now;

            }
            return base.SaveChangesAsync(cancellationToken);
        }

        //DbSet = Tablo
        public DbSet<Category> Categories { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Prompt> Prompts { get; set; }
        public DbSet<PromptCategory> PromptCategories { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }



    }
}
