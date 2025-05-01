using Microsoft.Extensions.Logging;

namespace PositionManagement.Shared.DependencyInjection.Extensions
{
    /// <summary>
    /// Contains extension methods to simplify logging configuration for the application.
    /// </summary>
    public static class LoggingExtensions
    {
        /// <summary>
        /// Configures the logging system to use console logging as the output.
        /// </summary>
        /// <param name="logging">The logging builder used to configure logging providers.</param>
        /// <returns>The updated logging builder with console logging configured.</returns>
        public static ILoggingBuilder AddConsoleLogging(this ILoggingBuilder logging)
        {
            // Remove all previously configured logging providers to ensure a clean configuration.
            logging.ClearProviders();

            // Add the console logging provider to enable logging output to the console.
            logging.AddConsole();

            // Return the updated logging builder for further configuration if needed.
            return logging;
        }
    }
}
