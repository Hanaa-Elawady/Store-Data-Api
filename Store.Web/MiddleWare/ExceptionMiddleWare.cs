using Store.Service.HandleResponse;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text.Json;

namespace Store.Web.MiddleWare
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _environment;
        private readonly ILogger<ExceptionMiddleWare> _logger;

        public ExceptionMiddleWare(RequestDelegate next , IHostEnvironment environment , ILogger<ExceptionMiddleWare> logger)
        {
            _next = next;
            _environment = environment;
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
                _logger.LogError(ex ,ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _environment.IsDevelopment()
                                ?new CustomExeption((int)HttpStatusCode.InternalServerError, ex.Message , ex.StackTrace)
                                :new CustomExeption((int)HttpStatusCode.InternalServerError, ex.Message);

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };

                var Json = JsonSerializer.Serialize(response,options);

                await context.Response.WriteAsync(Json);
            }

        }
    }
}
