using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp_Model_Library
{
    public class Product
    {
        public  int PropertyId { get; set; }
        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public double Price { get; set; }   

        public double Stock {  get; set; }
        public override string ToString()
        {
            return $"PropertyId: {PropertyId}, ProductName: {ProductName}, ProductDescription: {ProductDescription}, Price: {Price}, Stock: {Stock}";
        }


    }


}
