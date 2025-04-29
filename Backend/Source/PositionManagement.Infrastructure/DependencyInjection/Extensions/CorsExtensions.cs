using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PositionManagement.Infrastructure.DependencyInjection.Extensions;

namespace PositionManagement.Infrastructure.DependencyInjection.Extensions
{
    /// <summary>
    /// Provides extension methods for configuring and using CORS (Cross-Origin Resource Sharing) in the application.
    /// </summary>
    public static class CorsExtensions
    {
        // Name of the CORS policy used in the application
        private readonly static string _corsPolicyName = "PositionManagementCors";

        /// <summary>
        /// Adds CORS services to the application with a predefined policy.
        /// </summary>
        /// <param name="builder">The application builder used to configure services.</param>
        public static void AddCors(this IHostApplicationBuilder builder)
        {
            // Register CORS services with a specific policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(
                    name: _corsPolicyName,
                    builder =>
                    {
                        // Allow requests from any origin, with any method and any header
                        builder.WithOrigins("*")
                               .AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    }
                );
            });
        }

        /// <summary>
        /// Configures the application to use the predefined CORS policy.
        /// </summary>
        /// <param name="webApp">The web application instance.</param>
        public static void UseCors(WebApplication webApp)
        {
            // Apply the CORS policy to the application
            webApp.UseCors(_corsPolicyName);
        }
    }
}
