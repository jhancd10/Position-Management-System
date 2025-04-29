using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PositionManagement.Application.DependencyInjection;
using PositionManagement.Infrastructure.DependencyInjection.Extensions;
using PositionManagement.Shared.Common;

namespace PositionManagement.Infrastructure.DependencyInjection.Setup
{
    /// <summary>
    /// Provides methods to configure and run a default web application with predefined services, middleware, and configurations.
    /// </summary>
    public static class DefaultWebApplication
    {
        /// <summary>
        /// Creates and configures a new instance of a WebApplication.
        /// </summary>
        /// <param name="args">Command-line arguments passed to the application.</param>
        /// <param name="webappBuilder">Optional delegate to customize the WebApplicationBuilder.</param>
        /// <returns>A configured WebApplication instance ready to run.</returns>
        public static WebApplication Create(
            string[] args,
            Action<WebApplicationBuilder> webappBuilder = null)
        {
            // Create a new WebApplicationBuilder instance
            var builder = WebApplication.CreateBuilder(args);

            // Add logging configuration (console)
            builder.AddConsoleLogging();

            // Add Web API services and configurations to the application
            builder.AddWebApi();

            // Initial API Key application parameters
            var apiKeySection = builder.Configuration.GetSection("ApiKey");

            // Register application-level dependencies in the DI container
            builder.Services.AddApplicationDI();

            // Configure controllers and swagger with API key header filter
            builder.AddSwaggerServices(apiKeySection["Header"]);

            // Add Cors configuration
            builder.AddCors();

            // Bind ApiKey configuration to IOptions<ApiKey> in the DI container
            builder.Services.Configure<ApiKey>(apiKeySection);

            // Register the application's database context in the DI container
            builder.Services.AddDbContext();

            // Invoke the provided delegate to allow further customization of the WebApplicationBuilder
            webappBuilder?.Invoke(builder);

            /* Build the WebApplication instance using the configured WebApplicationBuilder.
             * This finalizes the setup and prepares the application for execution. */
            return builder.Build();
        }

        /// <summary>
        /// Runs the provided WebApplication instance by configuring middleware, routing, and starting the application.
        /// </summary>
        /// <param name="webApp">The WebApplication instance to run.</param>
        public static void Run(WebApplication webApp)
        {
            // Enable Swagger for API documentation and testing (Development Environment)
            SwaggerExtensions.UseSwagger(webApp);

            // Apply custom middleware to the application pipeline
            MiddlewareExtensions.UseMiddleware(webApp);

            // Enforce HTTPS redirection for secure communication
            webApp.UseHttpsRedirection();

            // Configure Cross-Origin Resource Sharing (CORS) policies
            CorsExtensions.UseCors(webApp);

            // Enable authorization middleware to handle user access control
            webApp.UseAuthorization();

            // Map controller endpoints to handle incoming HTTP requests
            webApp.MapControllers();

            // Start the web application and begin listening for requests
            webApp.Run();
        }
    }
}
