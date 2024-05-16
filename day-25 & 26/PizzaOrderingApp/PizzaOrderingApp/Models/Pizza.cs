using System.ComponentModel.DataAnnotations;

namespace PizzaOrderingApp.Models
{
    public class Pizza
    {
        [Key]
        public int PizzaId { get; set; }

        public string PizzaName { get; set; }

        public string PizzaFlavour { get; set;}

        public double Price { get; set; }

        public string Description { get; set; }

        public bool IsVegetarian { get; set; }

        public double Stock { get; set; }

        public List<OrderItems> OrderItems { get; set; } // Navigation property for order items
    }
}
