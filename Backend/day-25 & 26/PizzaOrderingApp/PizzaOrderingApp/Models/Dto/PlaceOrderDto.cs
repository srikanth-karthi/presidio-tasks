namespace PizzaOrderingApp.Models.Dto
{
    public class PlaceOrderDto
    {
        public List<OrderItemDTO> OrderItems { get; set; }
    }

    public class OrderItemDTO
    {
        public int PizzaId { get; set; }
        public int Quantity { get; set; }
    }
}
