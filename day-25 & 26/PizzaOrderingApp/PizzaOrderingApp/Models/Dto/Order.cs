namespace PizzaOrderingApp.Models.Dto
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public double TotalPrice { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }

    public class OrderItemDto
    {
        public int OrderItemId { get; set; }
        public int PizzaId { get; set; }
        public int Quantity { get; set; }
        public double  Price { get; set; }
        public PizzaDto Pizza { get; set; }
    }

    public class PizzaDto
    {
        public string PizzaName { get; set; }
        public string PizzaFlavour { get; set; }
        public bool IsVegetarian { get; set; }
    }

}
