using Application.Wrappers;
using System.Net;
using System.Text.Json;

namespace WebAPI.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context, ILogger<ExceptionMiddleware> logger)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Excepción no controlada");

                var result = Result.Fail(
                    HttpStatusCode.InternalServerError,
                    "Hubo un problema al procesar su solicitud. Intente más tarde.");

                var json = JsonSerializer.Serialize(result);
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)result.StatusCode;
                await response.WriteAsync(json);
            }
        }
    }
}
