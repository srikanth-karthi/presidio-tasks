using System.Runtime.Serialization;

namespace EmployeeRequestTrackerAPI.Exceptions
{
    [Serializable]
    internal class NoSuchEmployeeException : Exception
    {
        public NoSuchEmployeeException()
        {
        }

        public NoSuchEmployeeException(string? message) : base(message)
        {
        }

        public NoSuchEmployeeException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoSuchEmployeeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}