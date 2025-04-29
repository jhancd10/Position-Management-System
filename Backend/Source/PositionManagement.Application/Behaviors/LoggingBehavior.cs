using MediatR;
using Microsoft.Extensions.Logging;

namespace PositionManagement.Application.Behaviors
{
    /// <summary>
    /// LoggingBehavior is a pipeline behavior for MediatR that logs the handling and handled events
    /// of a request. It provides a way to track the execution of requests and their responses.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request being handled.</typeparam>
    /// <typeparam name="TResponse">The type of the response returned by the handler.</typeparam>
    public class LoggingBehavior<TRequest, TResponse>(
        ILogger<LoggingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
    {
        // Logger instance used to log information about the request and response
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger = logger;

        /// <summary>
        /// Handles the logging of the request before and after it is processed by the next handler in the pipeline.
        /// </summary>
        /// <param name="request">The request being handled.</param>
        /// <param name="next">The next delegate in the pipeline to process the request.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The response from the next handler in the pipeline.</returns>
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            // Log the start of request handling
            _logger.LogInformation(
                message: "Handling {RequestName}",
                typeof(TRequest).Name
            );

            // Call the next handler in the pipeline and await its response
            var response = await next(cancellationToken);

            // Log the completion of request handling
            _logger.LogInformation(
                message: "Handled {RequestName}",
                typeof(TRequest).Name
            );

            // Return the response from the next handler
            return response;
        }
    }
}
