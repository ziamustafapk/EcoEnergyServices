using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using YourProjectName.ViewModels;

namespace YourProjectName.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        

        public ExceptionMiddleware(RequestDelegate next)
        {
            
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            
            catch (Exception ex)
            {
                //Exception Logging
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var routeWhereExceptionOccured = context.Request.Path.Value;
            var path = JsonSerializer.Serialize(routeWhereExceptionOccured);
            string returnMessage = "";
            var customError = new CustomErrorViewModel()
            {
                Path = path

            };
            if (exception is AggregateException aggregateException)
            {
                var message = aggregateException.InnerExceptions.Select(e => e.Message).ToList();
                customError.ErrorMessages = message;
                returnMessage = JsonSerializer.Serialize(customError);

            }
            else
            {
                var message = exception.Message;
                customError.ErrorMessages = new List<string>{message};
                returnMessage = JsonSerializer.Serialize(customError);
            }
            
            
            string redirectUrl = $"/Home/Error?messageJson={returnMessage}";
            context.Response.Redirect(redirectUrl);
            //await context.Response.WriteAsync(message);


        }

    }

}
