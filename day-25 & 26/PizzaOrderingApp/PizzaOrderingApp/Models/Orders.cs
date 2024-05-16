using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PizzaOrderingApp.Models
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public Users User { get; set; }

        public double TotalPrice { get; set; }
    
        public List<OrderItems> OrderItems { get; set; }
    }
}
