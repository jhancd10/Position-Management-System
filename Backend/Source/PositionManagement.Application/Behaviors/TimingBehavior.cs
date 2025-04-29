using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace PositionManagement.Application.Behaviors
{
    /// <summary>
    /// Pipeline behavior for measuring the execution time of MediatR requests.
    /// Logs the time taken for a request to be processed.
    /// </summary>
    public class TimingBehavior<TRequest, TResponse>(
        ILogger<TimingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
    {
        // Logger instance for logging request timing information
        private readonly ILogger<TimingBehavior<TRequest, TResponse>> _logger = logger;

        /// <summary>
        /// Handles the request and measures the time taken for its execution.
        /// </summary>
        /// <param name="request">The incoming request object.</param>
        /// <param name="next">The next delegate in the pipeline.</param>
        /// <param name="cancellationToken">Token to observe for cancellation.</param>
        /// <returns>The response from the next delegate in the pipeline.</returns>
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            // Start a stopwatch to measure execution time
            var stopwatch = Stopwatch.StartNew();

            // Invoke the next delegate in the pipeline and await its response
            var response = await next(cancellationToken);

            // Stop the stopwatch after the request is processed
            stopwatch.Stop();

            // Log the time taken to process the request
            _logger.LogInformation(
                message: "Request {RequestName} completed in {ElapsedMilliseconds} ms",
                typeof(TRequest).Name,
                stopwatch.ElapsedMilliseconds
            );

            // Return the response from the next delegate
            return response;
        }
    }
}
