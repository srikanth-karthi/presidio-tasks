using ShoppingApp_Model_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingApp_BLL;
namespace ShoppingApp_Test
{
    public class CartTests
    {
        CartService cartService;
        OrderServices orderServices;
        Cart cart;
        CartItems cartItem1;
        CartItems cartItem2;

        ProductService productService;

        [SetUp]
        public void Setup()
        {
            cartService = new CartService();
            productService = new ProductService();
   
            Product product1 = new Product() { ProductName = "samopoo", ProductDescription = "higenic", Price = 30, Stock = 150 };
            Product product2 = new Product() { ProductName = "sofa", ProductDescription = "good sofa", Price = 500, Stock = 4 };
            productService.Add(product1);
            productService.Add(product2);
            cartItem1 = new CartItems() { Product= product1 , Quantity = 1 };
            cartItem2 = new CartItems() {  Product = product2, Quantity = 3 };
           cart= cartService.Add(new Cart() { UserName = "JohnDoe", CartItems = new List<CartItems>() });
        }



        [Test]
        public void AddToCartTest()
        {
           cartService.AddToCart(cart.UserId, cartItem1);
            Assert.AreEqual(1, cart.CartItems.Count);
            Assert.AreEqual(cartItem1, cart.CartItems[0]);
        }
        [Test]
        public void AddTest()
        {
            Cart data = cartService.Add(cart);
            Console.WriteLine(data.UserId);
            Cart data2 = cartService.Get(data.UserId);
            Assert.AreEqual(data, data2);
        }
        [Test]
        public void FAileAddToCartTest()
        {
            Assert.Throws<KeyNotFoundException>(() => cartService.AddToCart(100, cartItem1));
        }
        [Test]
        public void AddToCart_Failure_MaximumLimitReached()
        {
            cart.CartItems.Add(new CartItems() { Product = new Product(), Quantity = 4 });
            cart.CartItems.Add(new CartItems() { Product = new Product(), Quantity = 2 });

            Assert.Throws<MaximumLimitReachedException>(() => cartService.AddToCart(cart.UserId, cartItem2));
            Assert.AreEqual(2, cart.CartItems.Count); 
        }
        [Test]



        public void RemoveFromCartTest()
        {
            cart.CartItems.Add(cartItem1);
            cartService.RemoveFromCart(cart.UserId, 0);
            Assert.AreEqual(0, cart.CartItems.Count);
        }


        [Test]
        public void RemoveFromCart_Failure_ItemNotFound()
        {
            Assert.Throws<KeyNotFoundException>(() => cartService.RemoveFromCart(cart.UserId, 0));
        }



        [Test]
        public void CheckOutTest()
        {
            CartItems c=cartService.AddToCart(cart.UserId,cartItem1);


            double expectedPrice = cartItem1.Quantity * cartItem1.Product.Price +100;
      
    
            double finalPrice = cartService.CheckOut(cart.UserId);
      
            Assert.AreEqual(finalPrice, expectedPrice);
        }

        [Test]
        public void Failed_CheckOut()
        {
            Assert.Throws<KeyNotFoundException>(() => cartService.CheckOut(100));
        }

        [Test]

     
        public void CheckOut_DiscountApplied()
        {
            Cart data = cartService.Add(cart);

            cartService.AddToCart(data.UserId,cartItem1);
            cartService.AddToCart(data.UserId, cartItem1);
            cartService.AddToCart(data.UserId, cartItem2);


            double expectedPrice = ((cartItem1.Quantity * cartItem1.Product.Price) + (cartItem1.Quantity * cartItem1.Product.Price) + (cartItem2.Quantity * cartItem2.Product.Price)) * 0.95;
            
            double price = cartService.CheckOut(data.UserId);

            Assert.AreEqual(expectedPrice, price);
        }


    }
}
