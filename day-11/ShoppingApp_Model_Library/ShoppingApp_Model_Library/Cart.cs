namespace ShoppingApp_Model_Library
{
    public class Cart
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public List<CartItems> CartItems { get; set; }=new List<CartItems>();


    }
}
