using System.Runtime.Serialization;

namespace RequestTrackerApp.Exceptions
{
    [Serializable]
    internal class EmailAlreadyFoundException : Exception
    {
        public EmailAlreadyFoundException()
        {
        }

        public EmailAlreadyFoundException(string? message) : base(message)
        {
        }

        public EmailAlreadyFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EmailAlreadyFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}