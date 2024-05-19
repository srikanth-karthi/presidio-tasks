using System.Runtime.Serialization;

namespace RequestTrackerApp.Exceptions
{
    [Serializable]
    internal class AuthenticationError : Exception
    {
        public AuthenticationError()
        {
        }

        public AuthenticationError(string? message) : base(message)
        {
        }

        public AuthenticationError(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected AuthenticationError(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}