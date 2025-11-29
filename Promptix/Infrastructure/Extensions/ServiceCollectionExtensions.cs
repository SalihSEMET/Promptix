using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {

            services.AddDbContext<PromptixDbContext>(op =>
            {
                op.UseSqlServer(connectionString, b => b.MigrationsAssembly(typeof(PromptixDbContext).Assembly.FullName));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
}
