using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PositionManagement.Infrastructure.DependencyInjection.Extensions;
using PositionManagement.Shared.Common;

namespace PositionManagement.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Provides extension methods to configure dependency injection for the infrastructure layer.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Configures the infrastructure services and dependencies for the application.
        /// </summary>
        /// <param name="services">The service collection to which dependencies will be added.</param>
        /// <param name="configuration">The application configuration containing necessary settings.</param>
        /// <returns>The updated service collection with registered dependencies.</returns>
        public static IServiceCollection AddInfrastructureDI(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Bind ApiKey configuration to IOptions<ApiKey> in the DI container
            services.Configure<ApiKey>(configuration.GetSection("ApiKey"));

            // Add Swagger services and configure API key header filter
            services.AddSwaggerServices(configuration["ApiKey:Header"]);

            // Register the application's database context in the DI container
            services.AddDbContext(configuration);

            // Register the application's repositories in the DI container
            services.AddRepository();

            return services;
        }
    }
}
