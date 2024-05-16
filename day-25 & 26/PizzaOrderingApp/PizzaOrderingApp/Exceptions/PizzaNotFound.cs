using System.Runtime.Serialization;

namespace PizzaOrderingApp.Exceptions
{
    [Serializable]
    internal class PizzaNotFound : Exception
    {
        public PizzaNotFound()
        {
        }

        public PizzaNotFound(string? message) : base(message)
        {
        }

        public PizzaNotFound(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected PizzaNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}