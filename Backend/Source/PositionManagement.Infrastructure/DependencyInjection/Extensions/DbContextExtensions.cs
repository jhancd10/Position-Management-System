using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PositionManagement.Infrastructure.Data;

namespace PositionManagement.Infrastructure.DependencyInjection.Extensions
{
    /// <summary>
    /// Provides extension methods for configuring and adding DbContext instances to the service collection.
    /// </summary>
    public static class DbContextExtensions
    {
        /// <summary>
        /// Registers the PositionManagementDbContext with the service collection using an in-memory database.
        /// </summary>
        /// <param name="services">The service collection to which the DbContext will be added.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddDbContext(this IServiceCollection services)
        {
            /* Adds the PositionManagementDbContext to the service collection.
             * Configures it to use an in-memory database named "PositionManagementDb" */
            services.AddDbContext<PositionManagementDbContext>(
                options => options.UseInMemoryDatabase("PositionManagementDb")
            );

            // Returns the updated service collection.
            return services;
        }
    }
}
