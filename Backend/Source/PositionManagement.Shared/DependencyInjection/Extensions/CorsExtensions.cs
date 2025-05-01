using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace PositionManagement.Shared.DependencyInjection.Extensions
{
    /// <summary>
    /// Contains extension methods to configure and enable Cross-Origin Resource Sharing (CORS) in the application.
    /// </summary>
    public static class CorsExtensions
    {
        // Name of the CORS policy used in the application
        private readonly static string _corsPolicyName = "DefaultCors";

        /// <summary>
        /// Registers CORS services with a predefined policy in the application's service collection.
        /// </summary>
        /// <param name="services">The service collection to which CORS services will be added.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddDefaultCors(this IServiceCollection services)
        {
            // Add CORS services to the service collection
            services.AddCors(options =>
            {
                // Define a CORS policy with the specified name
                options.AddPolicy(
                    name: _corsPolicyName,
                    builder =>
                    {
                        // Configure the policy to allow any origin, method, and header
                        builder.WithOrigins("*")
                               .AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    }
                );
            });

            // Return the updated service collection
            return services;
        }

        /// <summary>
        /// Configures the application to use the predefined CORS policy.
        /// </summary>
        /// <param name="webApp">The web application instance to configure.</param>
        public static void UseDefaultCors(WebApplication webApp)
        {
            // Apply the CORS policy to the web application
            webApp.UseCors(_corsPolicyName);
        }
    }
}
