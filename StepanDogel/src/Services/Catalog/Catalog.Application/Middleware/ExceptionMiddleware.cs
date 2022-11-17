using Catalog.Application.Middleware.MiddlewareExtensions;
using Catalog.Application.Middleware.ServiceExceptions;
using Microsoft.AspNetCore.Http;

namespace Catalog.Application.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _nextRequest;

        public ExceptionMiddleware(RequestDelegate nextRequest)
        {
            this._nextRequest = nextRequest;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _nextRequest.Invoke(context);
            }
            catch (ServiceException ex)
            {
                switch (ex.Type)
                {
                    case ServiceErrorType.DifferentIds:
                        await ResponseExtension.CreateResponseAsync(context, StatusCodes.Status400BadRequest, "Different Ids");
                        break;

                    case ServiceErrorType.NoEntity:
                        await ResponseExtension.CreateResponseAsync(context, StatusCodes.Status400BadRequest, "No Entity with this id");
                        break;

                    case ServiceErrorType.InvalidId:
                        await ResponseExtension.CreateResponseAsync(context, StatusCodes.Status400BadRequest, "Invalid Id");
                        break;

                    default:
                        await ResponseExtension.CreateResponseAsync(context, StatusCodes.Status500InternalServerError, "Unknown Exeption");
                        break;
                }
            }
        }
    }
}
