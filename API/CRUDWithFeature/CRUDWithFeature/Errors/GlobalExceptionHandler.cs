using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CRUDWithFeature.Errors
{
    public class GlobalExceptionHandler: IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            this.logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError(exception, "Catch Error", exception.Message);

            var ProblemDetails = new ProblemDetails()
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Catch Erroe",
                Detail= exception.Message
            };
            httpContext.Response.StatusCode = ProblemDetails.Status.Value;
            await httpContext.Response.WriteAsJsonAsync(ProblemDetails);
            return true;
        }
    }
}
