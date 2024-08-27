using System.Runtime.Serialization;

namespace RequestTrackerApp.Exceptions
{
    [Serializable]
    internal class EmployeeNotFound : Exception
    {
        public EmployeeNotFound()
        {
        }

        public EmployeeNotFound(string? message) : base(message)
        {
        }

        public EmployeeNotFound(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EmployeeNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}