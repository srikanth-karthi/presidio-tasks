using System.Runtime.Serialization;

namespace ShoppingApp_BLL
{
    
    public class MaximumLimitReachedException : Exception
    {

 
        public MaximumLimitReachedException(string? message) : base(message)
        {
        }

  
    }
}