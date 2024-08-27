using PizzaOrderingApp.Models.Dto;
using System.ComponentModel.DataAnnotations;

namespace PizzaOrderingApp.Models
{
    public class Users: UserDTO
    {
        [Key]
        public int UserId { get; set; }
        public byte[] Password { get; set; }
        public byte[] HasCode { get; set; }
        

    }
}
