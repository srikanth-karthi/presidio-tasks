using RequestTrackerApp.Model;
using RequestTrackerApp.Models;
namespace RequestTrackerApp.Interfaces
{
    public interface ITokenService
    {

        public string GenerateToken(Employee user);
    }


    
}
