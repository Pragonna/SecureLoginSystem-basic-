using System.Net;

namespace Core.Domain.Exceptions
{
    public class HttpResponseException : Exception
    {
        public HttpResponseException(HttpStatusCode httpStatusCode, string message = null) : base($"Http status code: {httpStatusCode.ToString()}\n\n\n\nMessage: {message}")
        {
        }
    }
}
