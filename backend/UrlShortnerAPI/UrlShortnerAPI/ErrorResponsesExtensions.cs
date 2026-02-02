using Microsoft.AspNetCore.Diagnostics;

namespace UrlShortnerAPI
{
    internal static class ErrorResponsesExtensions
    {
        internal static void ConfigureErrorResponses(this WebApplication app)
        {
            app.UseExceptionHandler(exceptionHandlerApp => exceptionHandlerApp.Run(ExceptionHandler));
            app.UseStatusCodePages(statusCodeHandlerApp => statusCodeHandlerApp.Run(StatusCodeHandler));
        }

        private static async Task ExceptionHandler(HttpContext context)
        {
            var error = context.Features.Get<IExceptionHandlerFeature>()?.Error;
            var badRequestEx = error as BadHttpRequestException;
            var statusCode = badRequestEx?.StatusCode ?? StatusCodes.Status500InternalServerError;

            var result = TypedResults.Problem("Request processing failed.", null, statusCode);

            await result.ExecuteAsync(context);
        }

        private static async Task StatusCodeHandler(HttpContext httpContext)
        {
            if (httpContext.RequestServices.GetService<IProblemDetailsService>() is { } pds &&
                await pds.TryWriteAsync(new() { HttpContext = httpContext }))
            {
                return;
            }

            // Fallback
            await httpContext.Response.WriteAsync("Fallback: An error occurred.");
        }
    }
}
