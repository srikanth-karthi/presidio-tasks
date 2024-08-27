using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopping_DAL;
using ShoppingApp_Model_Library;

namespace ShoppingApp_BLL
{
    public class ProductService : AbstractRepository<Product, int>
    {
        public override async Task<Product> Get(int key) => items.FirstOrDefault(item => item.PropertyId == key) ?? throw new KeyNotFoundException("Item not found");

        public override async Task<Product> Add(Product item)
        {
            item.PropertyId = GenerateId();
            await base.Add(item);
            return item;
        }

        public override async Task<Product> Update(Product item)
        {
            Product product = await Get(item.PropertyId);

            if (product != null)
            {
                product = item;
                return await Get(item.PropertyId);
            }
            throw new KeyNotFoundException($"{item.PropertyId} not found");
        }
    }
}
