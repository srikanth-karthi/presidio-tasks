using Microsoft.EntityFrameworkCore;
using ProductApp.Context;
using ProductApp.Interfaces;
using ProductApp.Models;

namespace ProductApp.Repository
{
    public class ProductRepository : IRepository<int,Product>
    {
        private readonly ProductAppDbContext _productAppDbContext;
        public ProductRepository(ProductAppDbContext productAppDbContext) {
            _productAppDbContext = productAppDbContext;
        }

        public async Task<List<Product>> GetAll()
        {
            try
            {
                return await _productAppDbContext.Products.ToListAsync();
            }
            catch(Exception ex)
            {
                throw new Exception("Error in fetching products list");
            }
        }
    }
}
