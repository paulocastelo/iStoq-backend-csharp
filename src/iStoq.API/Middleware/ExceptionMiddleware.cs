public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado");
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var response = new
            {
                StatusCode = 500,
                Message = "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.",
                Detailed = _env.IsDevelopment() ? ex.Message : null
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
