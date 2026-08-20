namespace CRUD_WEBAPI.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;


    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred while processing the request.");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;

            var errorResponse = new
            {
                error = "An unexpected error occurred.",
                details = ex.Message
            };

            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}