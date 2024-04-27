using Shopping_DAL;
using ShoppingApp_Model_Library;

namespace ShoppingApp_BLL
{
    public class ProductService : AbstractRepository<Product, int>
    {
        public override Product Get(int key) => items.FirstOrDefault(item => item.PropertyId == key) ?? throw new KeyNotFoundException("Item not found");

        public override Product Add(Product item)
        {
            item.PropertyId = GenerateId();
            base.Add(item);
            return item;
        }

        public override Product Update(Product item)
        {
         
            Product product = Get(item.PropertyId);
          
            if (product!=null)
            {
                product = item;
                return Get(item.PropertyId);
            }
            throw new KeyNotFoundException($"{item.PropertyId} not found");
        }
    }


}

