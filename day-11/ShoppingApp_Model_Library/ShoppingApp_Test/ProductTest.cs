using ShoppingApp_BLL;
using ShoppingApp_Model_Library;
using System.Diagnostics;

namespace ShoppingApp_Test
{
    public class Tests
    {

        ProductService productService;

        Product product;
        Product product1 = new Product() { ProductName = "samopoo",ProductDescription = "higenic",Price = 30,Stock = 150};
        [SetUp]
        public void Setup()
        {
            productService = new ProductService();
            product = new Product() { ProductName="soap",ProductDescription="higenic",Price=35,Stock=100};
            productService.Add(product);
        }

        [Test]
        public void Addtest()
        {
           Product product= productService.Add(product1);

            Assert.AreEqual(product.PropertyId, product1.PropertyId);
        }
        [Test]
        public void Gettest()
        {
            Product product = productService.Get(1);

            Assert.AreEqual(product.PropertyId, 1);
        }
        [Test]
        public void Failed_Gettest()
        {
            Product product = productService.Get(1);

            Assert.Throws<KeyNotFoundException>(() => productService.Get(100));
        }
        [Test]
        public void Updatetest()
        {
             productService.Add(product1);
            product1.ProductName = "dove";
            Product product = productService.Update(product1);

            Assert.AreEqual(product.ProductName, product1.ProductName);
        }
        [Test]
        public void Failed_Updatetest()
        {
           Product product= productService.Add(product1);
            product.PropertyId = 1000;
            product.ProductName = "dove";
        
            Assert.Throws<KeyNotFoundException>(() => productService.Update(product1));
        }
        [Test]
        public void Deletetest()
        {
          bool Boolean=  productService.Delete(product);
            Assert.IsTrue(Boolean);
        }
        public void Failed_Deletetest()
        {
            Assert.Throws<KeyNotFoundException>(() => productService.Delete(product1));
        }


    }
}