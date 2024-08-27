using System.Runtime.Serialization;

namespace DoctorClinicApi.Exceptions
{
    [Serializable]
    internal class DoctorNotFoundExceptions : Exception
    {
        public DoctorNotFoundExceptions()
        {
        }

        public DoctorNotFoundExceptions(string? message) : base(message)
        {
        }

        public DoctorNotFoundExceptions(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DoctorNotFoundExceptions(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}