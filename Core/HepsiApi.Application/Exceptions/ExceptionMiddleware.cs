using FluentValidation;
using Microsoft.AspNetCore.Http;
using SendGrid.Helpers.Errors.Model; 

namespace HepsiApi.Application.Exceptions
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsnyc(context, ex);
            }
        }
        private static Task HandleExceptionAsnyc(HttpContext context, Exception exception)
        {
            var statusCode = GetStatusCode(exception);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            if(exception.GetType() == typeof(ValidationException))
            {
                return context.Response.WriteAsync(new ExceptionModel
                {
                    Errors = ((ValidationException)exception).Errors.Select(x=>x.ErrorMessage),
                    StatusCode = StatusCodes.Status400BadRequest
                }.ToString());
            }
            List<string> errors = new()
            {
                $"Hata Mesajı: {exception.Message}",
                $"Mesaj Açıklaması: {exception.InnerException}", 
            };
            return context.Response.WriteAsync(text: new ExceptionModel
            {
                Errors = errors,
                StatusCode = statusCode
            }.ToString());
        }
        private static int GetStatusCode(Exception exception) =>
            exception switch
            {
                BadRequestException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                ValidationException => StatusCodes.Status422UnprocessableEntity,
                _ => StatusCodes.Status500InternalServerError
            };
    }
}
