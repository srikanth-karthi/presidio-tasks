using System.ComponentModel.DataAnnotations;

namespace PizzaOrderingApp.Models.Dto
{
    public class UserDTO: LoginDTO
    {
   
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
