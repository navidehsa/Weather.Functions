
namespace Weather.Functions.ExceptionExtensions
{
    public class ServiceException : SemanticExceptionBase
    {
        public HttpResponseMessage ResponseMessage { get; }

        public ServiceException(HttpResponseMessage responseMessage, string message) : base(message, "HttpServiceError")
        {
            ResponseMessage = responseMessage;
        }

        public ServiceException(HttpResponseMessage responseMessage, string message, Exception cause) : base(message, "HttpServiceError", cause)
        {
            ResponseMessage = responseMessage;
        }
    }
}
