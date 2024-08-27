using System.Runtime.Serialization;

namespace PizzaOrderingApp.Exceptions
{
    [Serializable]
    internal class NoStockException : Exception
    {
        public NoStockException()
        {
        }

        public NoStockException(string? message) : base(message)
        {
        }

        public NoStockException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoStockException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}