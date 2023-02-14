using CarRent.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data.Common;

namespace CarRent.IntegrationTests.Factories
{
    public class CarRentWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DatabaseContext>));
                if (descriptor != null) services.Remove(descriptor);

                services.AddDbContext<DatabaseContext>(options => {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                });
            });
        }
    }
}
