using System.Runtime.Serialization;

namespace PizzaOrderingApp.Exceptions
{
    [Serializable]
    internal class OrderItemNotFoundException : Exception
    {
        public OrderItemNotFoundException()
        {
        }

        public OrderItemNotFoundException(string? message) : base(message)
        {
        }

        public OrderItemNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected OrderItemNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}