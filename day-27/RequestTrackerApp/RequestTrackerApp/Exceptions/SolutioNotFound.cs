using System.Runtime.Serialization;

namespace RequestTrackerApp.Exceptions
{
    [Serializable]
    internal class SolutioNotFound : Exception
    {
        public SolutioNotFound()
        {
        }

        public SolutioNotFound(string? message) : base(message)
        {
        }

        public SolutioNotFound(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected SolutioNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}