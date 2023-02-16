using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration.Internal;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRent.Database.Interfaces;
using CarRent.Database.Interfaces.Repositories;
using CarRent.Database.Repositories;
using System.Reflection;
using FitnessApp.Database.Base;
using CarRent.Database.Interfaces.Base;

namespace CarRent.Database.Configuration
{
    public static class DatabaseServiceExtensions
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IDatabaseContext, DatabaseContext>(options =>
            {
                options.UseInMemoryDatabase("CarRent");
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            var typesToRegister = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(type => !string.IsNullOrEmpty(type.Namespace)
                    && type.BaseType != null
                    && type.BaseType.IsGenericType
                    && type.BaseType.GetGenericTypeDefinition() == typeof(BaseRepository<>));

            foreach (var type in typesToRegister)
            {
                var implementedInterface = type.GetInterfaces()
                    .Where(x => x.GetInterfaces().Any(y => y.GetGenericTypeDefinition() == typeof(IBaseRepository<>)))
                    .First();

                services.AddTransient(implementedInterface, type);
            }

            return services;
        }
    }
}
