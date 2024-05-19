using System.Runtime.Serialization;

namespace RequestTrackerApp.Exceptions
{
    [Serializable]
    internal class RequestNotFound : Exception
    {
        public RequestNotFound()
        {
        }

        public RequestNotFound(string? message) : base(message)
        {
        }

        public RequestNotFound(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected RequestNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}