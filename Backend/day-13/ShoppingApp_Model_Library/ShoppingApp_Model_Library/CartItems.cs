using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp_Model_Library
{
    public class CartItems
    {
        public int CartItemsId { get; set; }


        public Product Product { get; set; }

        public double Discount { get; set; }
        public DateTime PriceExpiryDate { get; set; }

        public int Quantity { get; set; }



    }
}
