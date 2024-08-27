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
        public async Task  Setup()
        {
            productService = new ProductService();
            product = new Product() { ProductName="soap",ProductDescription="higenic",Price=35,Stock=100};
           await productService.Add(product);
        }

        [Test]
        public async Task Addtest()
        {
           Product product= await productService.Add(product1);

            Assert.AreEqual(product.PropertyId, product1.PropertyId);
        }
        [Test]
        public async Task Gettest()
        {
            Product product = await productService.Get(1);

            Assert.AreEqual(product.PropertyId, 1);
        }
        [Test]
        public async Task Failed_Gettest()
        {
            Product product = await productService.Get(1);

            Assert.ThrowsAsync<KeyNotFoundException>(async () => await productService.Get(100));
        }
        [Test]
        public async Task Updatetest()
        {
            await productService.Add(product1);
            product1.ProductName = "dove";
            Product product =await productService.Update(product1);

            Assert.AreEqual(product.ProductName, product1.ProductName);
        }

        [Test]
        public async Task Deletetest()
        {
          bool Boolean= await productService.Delete(product);
            Assert.IsTrue(Boolean);
        }
        public async Task Failed_Deletetest()
        {
            Assert.Throws<KeyNotFoundException>(() => productService.Delete(product1));
        }


    }
}