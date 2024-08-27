using System.Runtime.Serialization;

namespace PizzaOrderingApp.Exceptions
{
    [Serializable]
    internal class InsufficientQundityExecution : Exception
    {
        public InsufficientQundityExecution()
        {
        }

        public InsufficientQundityExecution(string? message) : base(message)
        {
        }

        public InsufficientQundityExecution(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InsufficientQundityExecution(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}