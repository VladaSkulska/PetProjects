using System.Net;

namespace HDrezka.Utilities.Exceptions
{
    public class TicketOperationException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public TicketOperationException() : base() { }

        public TicketOperationException(string message) : base(message) { }
        public TicketOperationException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}