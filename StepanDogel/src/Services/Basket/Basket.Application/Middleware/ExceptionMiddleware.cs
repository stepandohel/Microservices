using Basket.Application.Middleware.Exceptions;
using Basket.Application.Middleware.ServiceExceptions;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace Basket.Application.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            this._next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (ServiceException ex)
            {
                switch (ex.Type)
                {
                    case ServiceErrorType.DifferentIds:
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        byte[] differentIdResponseString = Encoding.UTF8.GetBytes("Different Ids");
                        context.Response.ContentType = "application/json";
                        await context.Response.Body.WriteAsync(differentIdResponseString, 0, differentIdResponseString.Length);
                        break;
                    case ServiceErrorType.NoEntity:
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        byte[] noEntityResponseString = Encoding.UTF8.GetBytes("No Entity with this id");
                        context.Response.ContentType = "application/json";
                        await context.Response.Body.WriteAsync(noEntityResponseString, 0, noEntityResponseString.Length);
                        break;
                }

            }
            catch
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                byte[] data = Encoding.UTF8.GetBytes("Bad request");
                await context.Response.Body.WriteAsync(data, 0, data.Length);
            }
        }
    }
}
