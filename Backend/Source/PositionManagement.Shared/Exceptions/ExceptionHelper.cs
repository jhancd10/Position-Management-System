namespace PositionManagement.Shared.Exceptions
{
    /// <summary>
    /// Provides helper methods for working with exceptions, including extracting detailed error messages.
    /// </summary>
    public static class ExceptionHelper
    {
        /// <summary>
        /// Retrieves the full error message from an exception, including messages from inner exceptions if present.
        /// </summary>
        /// <param name="ex">The exception to extract the message from.</param>
        /// <returns>A string containing the full error message.</returns>
        public static string GetFullMessage(Exception ex)
        {
            return ex.InnerException == null ?
                ex.Message :
                ex.Message + " --> " + (ex.InnerException.InnerException == null ?
                    ex.InnerException.Message :
                    ex.InnerException.InnerException.Message
                );
        }
    }
}
