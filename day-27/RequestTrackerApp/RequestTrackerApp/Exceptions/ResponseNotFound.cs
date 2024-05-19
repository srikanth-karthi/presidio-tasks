using System.Runtime.Serialization;

namespace RequestTrackerApp.Exceptions
{
    [Serializable]
    internal class ResponseNotFound : Exception
    {
        public ResponseNotFound()
        {
        }

        public ResponseNotFound(string? message) : base(message)
        {
        }

        public ResponseNotFound(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ResponseNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}