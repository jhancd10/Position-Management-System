using Microsoft.Extensions.DependencyInjection;

namespace PositionManagement.Shared.DependencyInjection.Extensions
{
    /// <summary>
    /// Contains extension methods to simplify the configuration of Web API services in the application.
    /// </summary>
    public static class WebApiExtensions
    {
        /// <summary>
        /// Registers services required for Web API functionality, including controllers and API documentation.
        /// </summary>
        /// <param name="services">The service collection to which the Web API services will be added.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddWebApi(this IServiceCollection services)
        {
            // Register controllers to enable handling of HTTP requests
            services.AddControllers();

            // Register API explorer to enable endpoint discovery and documentation generation
            services.AddEndpointsApiExplorer();

            return services;
        }
    }
}
