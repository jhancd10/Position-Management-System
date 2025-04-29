using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PositionManagement.Infrastructure.DependencyInjection.Extensions
{
    /// <summary>
    /// Provides extension methods for configuring Web API services in the application.
    /// </summary>
    public static class WebApiExtensions
    {
        /// <summary>
        /// Configures the application to use Web API services, including controllers and API explorer endpoints.
        /// </summary>
        /// <param name="builder">The application builder used to configure services.</param>
        public static void AddWebApi(this IHostApplicationBuilder builder)
        {
            // Adds support for controllers in the application
            builder.Services.AddControllers();

            // Adds support for API endpoint exploration and documentation
            builder.Services.AddEndpointsApiExplorer();
        }
    }
}
