using Microsoft.Extensions.DependencyInjection;
using PositionManagement.Infrastructure.Filters;

namespace PositionManagement.Infrastructure.DependencyInjection.Extensions
{
    /// <summary>
    /// Contains extension methods to configure Swagger services and customize API documentation.
    /// </summary>
    public static class SwaggerExtensions
    {
        /// <summary>
        /// Configures Swagger services and adds a custom API key header filter for enhanced security.
        /// </summary>
        /// <param name="services">The service collection to which Swagger services will be added.</param>
        /// <param name="apiKeyHeaderName">The name of the API key header to include in Swagger documentation.</param>
        /// <returns>The updated service collection with Swagger services configured.</returns>
        public static IServiceCollection AddSwaggerServices(
            this IServiceCollection services,
            string apiKeyHeaderName)
        {
            // Register Swagger generation services in the dependency injection container
            services.AddSwaggerGen(c =>
            {
                // Apply a custom operation filter to include the specified API key header in Swagger documentation
                c.OperationFilter<ApiKeyHeader>(apiKeyHeaderName);
            });

            // Return the updated service collection
            return services;
        }
    }
}
