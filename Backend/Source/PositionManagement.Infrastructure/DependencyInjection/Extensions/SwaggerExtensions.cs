using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PositionManagement.Infrastructure.Filters;

namespace PositionManagement.Infrastructure.DependencyInjection.Extensions
{
    /// <summary>
    /// Provides extension methods to configure and use Swagger in the application.
    /// </summary>
    public static class SwaggerExtensions
    {
        /// <summary>
        /// Adds Swagger services to the application, including an API key header filter.
        /// </summary>
        /// <param name="builder">The application builder.</param>
        /// <param name="apiKeyHeaderName">The name of the API key header to be used in Swagger.</param>
        public static void AddSwaggerServices(
            this IHostApplicationBuilder builder,
            string apiKeyHeaderName)
        {
            // Add Swagger generation services to the dependency injection container
            builder.Services.AddSwaggerGen(c =>
            {
                // Add a custom operation filter to include the API key header in Swagger documentation
                c.OperationFilter<ApiKeyHeader>(apiKeyHeaderName);
            });
        }

        /// <summary>
        /// Configures the application to use Swagger and Swagger UI in the development environment.
        /// </summary>
        /// <param name="webApp">The web application instance.</param>
        public static void UseSwagger(WebApplication webApp)
        {
            // Check if the application is running in the development environment
            if (webApp.Environment.IsDevelopment())
            {
                // Enable the Swagger middleware to serve the generated Swagger JSON
                webApp.UseSwagger();

                // Enable the Swagger UI middleware to serve the Swagger UI
                webApp.UseSwaggerUI();
            }
        }
    }
}
