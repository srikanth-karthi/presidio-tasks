using ProductApp.Interfaces;
using ProductApp.Models;

namespace ProductApp.Services
{
    public class ProductServiceBL : IProductService
    {
        private readonly IRepository<int, Product> _repository;

        public ProductServiceBL(IRepository<int, Product> repository)
        {
            _repository = repository;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            try
            {
                return await _repository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in getting products list");
            }
        }
    }
}
