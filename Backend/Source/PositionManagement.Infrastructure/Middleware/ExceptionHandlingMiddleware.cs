using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PositionManagement.Shared.Exceptions;

namespace PositionManagement.Infrastructure.Middleware
{
    /// <summary>
    /// Middleware for handling exceptions globally in the application.
    /// Captures and processes exceptions, logging them and returning appropriate HTTP responses.
    /// </summary>
    public class ExceptionHandlingMiddleware(
        ILogger<ExceptionHandlingMiddleware> logger,
        RequestDelegate next)
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

        private readonly RequestDelegate _next = next;

        /// <summary>
        /// Invokes the middleware to process the HTTP request and handle any exceptions that occur.
        /// </summary>
        /// <param name="context">The current HTTP context.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch (ValidationException validationEx)
            {
                /// <summary>
                /// Handles validation exceptions by logging the error and returning a 400 Bad Request response.
                /// </summary>
                _logger.LogWarning(
                    message: "Validation error: {exception}",
                    ExceptionHelper.GetFullMessage(validationEx)
                );

                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var errors = validationEx.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );

                await context.Response.WriteAsJsonAsync(new { message = "Invalid request", errors });
            }

            catch (NotFoundException notFoundEx)
            {
                /// <summary>
                /// Handles not found exceptions by logging the error and returning a 404 Not Found response.
                /// </summary>
                _logger.LogInformation(
                    message: "Resource not found: {exception}",
                    ExceptionHelper.GetFullMessage(notFoundEx)
                );

                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsJsonAsync(new { message = notFoundEx.Message });
            }

            catch (Exception ex)
            {
                /// <summary>
                /// Handles unexpected exceptions by logging the error and returning a 500 Internal Server Error response.
                /// </summary>
                _logger.LogError(
                    message: "Unexpected error: {exception}",
                    ExceptionHelper.GetFullMessage(ex)
                );

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new { message = "An error occurred on the server" });
            }
        }
    }
}
