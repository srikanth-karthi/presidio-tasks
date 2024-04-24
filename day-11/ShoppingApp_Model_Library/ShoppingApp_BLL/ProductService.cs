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
            int index = items.ToList().FindIndex(p => p.PropertyId == item.PropertyId);
        
            if (index != -1)
            {
                items[index] = item;
                return item;
            }
            throw new KeyNotFoundException($"{item.PropertyId} not found");
        }
    }


}

