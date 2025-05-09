using FluentValidation;
using MediatR;

namespace PositionManagement.Application.Behaviors
{
    /// <summary>
    /// Pipeline behavior for validating requests using FluentValidation before they are processed
    /// </summary>
    public class ValidationBehaviour<TRequest, TResponse>(
        IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        // Collection of validators for the request
        private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

        /// <summary>
        /// Handles the validation of the request and proceeds to the next behavior in the pipeline
        /// </summary>
        /// <param name="request">The incoming request to validate</param>
        /// <param name="next">The next delegate in the pipeline</param>
        /// <param name="cancellationToken">Token to observe for cancellation</param>
        /// <returns>The response from the next behavior in the pipeline</returns>
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            // Check if there are any validators for the request
            if (_validators.Any())
            {
                // Create a validation context for the request
                var context = new ValidationContext<TRequest>(request);

                // Execute all validators asynchronously and collect their results
                var validationResults = await Task.WhenAll(
                    _validators.Select(v =>
                        v.ValidateAsync(context, cancellationToken)));

                // Collect all validation failures
                var failures = validationResults
                    .Where(r => r.Errors.Count != 0)
                    .SelectMany(r => r.Errors)
                    .ToList();

                // Throw a validation exception if there are any failures
                if (failures.Count != 0) throw new ValidationException(failures);
            }

            // Proceed to the next behavior in the pipeline
            return await next(cancellationToken);
        }
    }
}
