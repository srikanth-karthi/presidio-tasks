using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp_Model_Library
{
    public class Orders
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public double Quantity { get; set; }

        public double Price { get; set; }
    }
}
