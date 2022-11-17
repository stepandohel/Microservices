using Microsoft.AspNetCore.Http;
using System.Text;

namespace Basket.Application.Middleware.MiddlewareExtensions
{
    internal class ResponseExtension
    {
        internal static async Task<HttpContext> CreateResponseAsync(HttpContext context, int statusCode, string rensonseString)
        {
            context.Response.StatusCode = statusCode;
            byte[] differentIdResponseString = Encoding.UTF8.GetBytes(rensonseString);
            context.Response.ContentType = "application/json";
            await context.Response.Body.WriteAsync(differentIdResponseString, 0, differentIdResponseString.Length);

            return context;
        }
    }
}
