using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PositionManagement.Infrastructure.DependencyInjection.Extensions
{
    /// <summary>
    /// Provides extension methods for configuring logging in the application.
    /// </summary>
    public static class LoggingExtensions
    {
        /// <summary>
        /// Configures the application to use console logging.
        /// </summary>
        /// <param name="builder">The application builder used to configure the host.</param>
        public static void AddConsoleLogging(this IHostApplicationBuilder builder)
        {
            // Clear all existing logging providers to start with a clean slate
            builder.Logging.ClearProviders();

            // Add the console logging provider to enable logging to the console
            builder.Logging.AddConsole();
        }
    }
}
