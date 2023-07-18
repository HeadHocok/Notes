using Microsoft.AspNetCore.Builder;

namespace Notes.WebApi.Middleware
{
    /// <summary>
    /// Добавляем обработчик ошибок (Middleware) в пайплайн.
    /// </summary>
    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this
            IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}