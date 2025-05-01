using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using PositionManagement.Shared.DependencyInjection.Extensions;

namespace PositionManagement.Shared.DependencyInjection
{
    /// <summary>
    /// Provides utility methods to simplify the creation and execution of a web application 
    /// with default configurations, services, and middleware.
    /// </summary>
    public static class DefaultWebApplication
    {
        /// <summary>
        /// Creates and configures a new instance of a WebApplication with default settings.
        /// </summary>
        /// <param name="args">Command-line arguments for the application.</param>
        /// <param name="webAppBuilder">An optional action to customize the WebApplicationBuilder.</param>
        /// <returns>A configured WebApplication instance.</returns>
        public static WebApplication Create(
            string[] args,
            Action<WebApplicationBuilder> webAppBuilder = null)
        {
            // Create a new WebApplicationBuilder instance using the provided arguments
            var builder = WebApplication.CreateBuilder(args);

            // Configure logging to output messages to the console
            builder.Logging.AddConsoleLogging();

            // Register default Web API services and configurations
            builder.Services.AddWebApi();

            // Register default CORS policies
            builder.Services.AddDefaultCors();

            // Allow further customization of the WebApplicationBuilder via the provided delegate
            webAppBuilder?.Invoke(builder);

            // Build and return the configured WebApplication instance
            return builder.Build();
        }

        /// <summary>
        /// Configures and starts the provided WebApplication instance with default middleware and routing.
        /// </summary>
        /// <param name="webApp">The WebApplication instance to configure and run.</param>
        /// <param name="configureMiddleware">An optional action to add custom middleware to the pipeline.</param>
        public static void Run(
            WebApplication webApp,
            Action<WebApplication> configureMiddleware = null)
        {
            // Check if the application is running in a development environment
            if (webApp.Environment.IsDevelopment())
            {
                // Enable Swagger for API documentation and testing
                webApp.UseSwagger();
                webApp.UseSwaggerUI();
            }

            // Apply custom middleware if provided
            configureMiddleware?.Invoke(webApp);

            // Enforce HTTPS redirection for secure communication
            webApp.UseHttpsRedirection();

            // Apply default CORS policies to the application
            CorsExtensions.UseDefaultCors(webApp);

            // Enable authorization middleware to handle access control
            webApp.UseAuthorization();

            // Map controller endpoints to handle incoming HTTP requests
            webApp.MapControllers();

            // Start the application and begin listening for incoming requests
            webApp.Run();
        }
    }
}
