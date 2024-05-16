using PizzaOrderingApp.Models;

namespace PizzaOrderingApp.Interfaces
{
    public interface ITokenService
    {

        public string GenerateToken(Users user);
    }


    
}
