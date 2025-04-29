using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PositionManagement.Shared.Common;

namespace PositionManagement.Infrastructure.Middleware
{
    /// <summary>
    /// Middleware to handle API key authentication for incoming HTTP requests.
    /// Validates the presence and correctness of the API key in the request headers.
    /// </summary>
    public class ApiKeyMiddleware
    {
        private readonly string API_KEY_HEADER;
        private readonly string VALID_API_KEY;

        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiKeyMiddleware"/> class.
        /// </summary>
        /// <param name="apiKey">The API key configuration containing the header name and valid key value.</param>
        /// <param name="next">The next middleware in the request pipeline.</param>
        public ApiKeyMiddleware(
            IOptions<ApiKey> apiKey,
            RequestDelegate next)
        {
            _next = next;

            API_KEY_HEADER = apiKey.Value.Header;
            VALID_API_KEY = apiKey.Value.Key;
        }

        /// <summary>
        /// Processes the incoming HTTP request to validate the API key.
        /// If the API key is missing or invalid, the request is rejected with an appropriate status code.
        /// </summary>
        /// <param name="context">The HTTP context of the current request.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(API_KEY_HEADER, out var extractedKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("API Key required");
                return;
            }

            if (!VALID_API_KEY.Equals(extractedKey))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Invalid API Key");
                return;
            }

            await _next(context);
        }
    }
}
