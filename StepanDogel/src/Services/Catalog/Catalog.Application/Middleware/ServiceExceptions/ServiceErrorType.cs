namespace Catalog.Application.Middleware.ServiceExceptions
{
    public enum ServiceErrorType
    {
        DifferentIds = 1000,
        NoEntity = 1001,
        InvalidId = 1002,
        UnknownException = 1999
    }
}
