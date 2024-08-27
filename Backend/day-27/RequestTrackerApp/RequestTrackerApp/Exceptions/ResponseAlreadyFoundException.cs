using System.Runtime.Serialization;

namespace RequestTrackerApp.Exceptions
{
    [Serializable]
    internal class ResponseAlreadyFoundException : Exception
    {
        public ResponseAlreadyFoundException()
        {
        }

        public ResponseAlreadyFoundException(string? message, Model.SolutionResposnse existResponse) : base(message)
        {
        }

        public ResponseAlreadyFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ResponseAlreadyFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}