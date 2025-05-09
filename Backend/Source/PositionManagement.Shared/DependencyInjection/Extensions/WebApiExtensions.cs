using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace PositionManagement.Shared.DependencyInjection.Extensions
{
    /// <summary>
    /// Provides extension methods to configure and enhance Web API services in the application
    /// </summary>
    public static class WebApiExtensions
    {
        /// <summary>
        /// Configures services required for Web API functionality, including controllers and API documentation
        /// </summary>
        /// <param name="services">The service collection to which the Web API services will be added</param>
        /// <returns>The updated service collection</returns>
        public static IServiceCollection AddWebApi(this IServiceCollection services)
        {
            /* Add controllers to handle HTTP requests and 
             * Configure JSON serializer to ignore reference cycles */
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });

            // Add API explorer to enable endpoint discovery and documentation generation
            services.AddEndpointsApiExplorer();

            // Return the updated service collection
            return services;
        }
    }
}
