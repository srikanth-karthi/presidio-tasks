using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaOrderingApp.Models
{
    public class OrderItems
    {
        [Key]
        public int OrderItemsId { get; set; }

        [ForeignKey("PizzaId")]
        public int PizzaId { get; set; }
        public Pizza Pizza { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        [ForeignKey("OrderId")]  // Foreign key attribute for OrderId
        public int OrderId { get; set; }  // Foreign key property
        public Orders Order { get; set; }  // Navigation property

    }
}
