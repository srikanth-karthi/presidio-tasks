using ProductApp.Models;

namespace ProductApp.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
    }
}
