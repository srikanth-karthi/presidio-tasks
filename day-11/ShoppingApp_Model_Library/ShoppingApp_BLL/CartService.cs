using Shopping_DAL;
using ShoppingApp_Model_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp_BLL
{
    public class CartService : AbstractRepository<Cart, int>
    {
        readonly ProductService ProductService;
        readonly OrderServices OrderService;
        public CartService() {
            ProductService  = new ProductService();
            OrderService = new OrderServices();
        }
        public override Cart Get(int key) => items.FirstOrDefault(item => item.UserId == key) ?? throw new KeyNotFoundException("Item not found");

        public override Cart Add(Cart item)
        {
            item.UserId = GenerateId();
            base.Add(item);
            return item;
        }


        public override Cart Update(Cart item)
        {
            int index = items.ToList().FindIndex(p => p.UserId == item.UserId);

            if (index != -1)
            {
                items[index] = item;
                return item;
            }
            throw new KeyNotFoundException($"{item.UserId} not found");

        }

        public CartItems AddToCart(int key, CartItems item)
        {
            Cart user = Get(key);

            if (user.CartItems.Sum(cartItem => cartItem.Quantity) + item.Quantity <= 6)
            {

                item.CartItemsId=GenerateId();
                user.CartItems.Add(item);
            }


            else throw new MaximumLimitReachedException("maximun card Quantity Reached");

            return item;
        }


        public void RemoveFromCart(int key, int CartItemsId)
        {
            Cart user = Get(key);
            if (!user.CartItems.Remove(user.CartItems.FirstOrDefault(item => item.CartItemsId == CartItemsId))) throw new KeyNotFoundException();
        }

        public double CheckOut(int key)
        {

            Cart user = Get(key);
            double finalPrice = 0;

            foreach (var cartItem in user.CartItems)
            {
                Product product = ProductService.Get(cartItem.Product.PropertyId);
                finalPrice += cartItem.Quantity * product.Price;
                product.Stock -= cartItem.Quantity;
                OrderService.Add(new Orders { ProductName = product.ProductName, Price = cartItem.Quantity * product.Price, Quantity = cartItem.Quantity, ProductId = product.PropertyId });
            }

            if (finalPrice < 100) finalPrice += 100;
            else if (user.CartItems.Count() >= 3 && finalPrice >= 1500) finalPrice = finalPrice*0.95;
            Console.WriteLine(finalPrice);
            return finalPrice;
        }








    }
}
