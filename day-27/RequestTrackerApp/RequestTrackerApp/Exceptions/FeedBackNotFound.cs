using System.Runtime.Serialization;

namespace RequestTrackerApp.Exceptions
{
    [Serializable]
    internal class FeedBackNotFound : Exception
    {
        public FeedBackNotFound()
        {
        }

        public FeedBackNotFound(string? message) : base(message)
        {
        }

        public FeedBackNotFound(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected FeedBackNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}