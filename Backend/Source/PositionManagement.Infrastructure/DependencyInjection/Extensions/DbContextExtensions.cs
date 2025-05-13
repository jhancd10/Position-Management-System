using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PositionManagement.Infrastructure.Data;
using PositionManagement.Infrastructure.Data.Seed;

namespace PositionManagement.Infrastructure.DependencyInjection.Extensions
{
    /// <summary>
    /// Provides extension methods for configuring and managing the PositionManagementDbContext
    /// </summary>
    public static class DbContextExtensions
    {
        /// <summary>
        /// Adds the PositionManagementDbContext to the service collection using an in-memory database
        /// </summary>
        /// <param name="services">The service collection to which the DbContext will be added</param>
        /// <returns>The updated service collection</returns>
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            // Adds the PositionManagementDbContext to the service collection
            // Configures it to use an in-memory database named "PositionManagementDb"
            //services.AddDbContext<PositionManagementDbContext>(
            //    options => options.UseInMemoryDatabase("PositionManagementDb")
            //);

            services.AddDbContext<PositionManagementDbContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("PositionManagementConnection"), 
                    sql => sql.MigrationsAssembly("PositionManagement.Infrastructure")
                )
            );

            // Returns the updated service collection
            return services;
        }

        /// <summary>
        /// Seeds the database with initial data for the PositionManagementDbContext
        /// </summary>
        /// <param name="webApp">The WebApplication instance used to access the service provider</param>
        public static void SeedDatabase(WebApplication webApp)
        {
            // Creates a new scope for resolving scoped services
            using var scope = webApp.Services.CreateScope();

            // Resolves the PositionManagementDbContext from the service provider
            var context = scope.ServiceProvider.GetRequiredService<PositionManagementDbContext>();

            // Ensures the database is created
            context.Database.EnsureCreated();

            // Seeds the database with initial data
            context.Seed();
        }
    }
}
