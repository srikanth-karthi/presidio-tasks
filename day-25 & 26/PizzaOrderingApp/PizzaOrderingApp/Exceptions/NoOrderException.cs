using System.Runtime.Serialization;

namespace PizzaOrderingApp.Exceptions
{
    [Serializable]
    internal class NoOrderException : Exception
    {
        public NoOrderException()
        {
        }

        public NoOrderException(string? message) : base(message)
        {
        }

        public NoOrderException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoOrderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}