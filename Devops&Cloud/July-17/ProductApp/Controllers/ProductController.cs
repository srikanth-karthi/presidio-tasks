using Microsoft.AspNetCore.Mvc;
using ProductApp.Interfaces;
using ProductApp.Models;
using ProductApp.Models.DTO;

namespace ProductApp.Controllers
{
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getAllProducts")]

        public async Task<ActionResult<List<Product>>> GetProductList()
        {
            try
            {
                var result = await _productService.GetAllProducts();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(501 , ex.Message));
            }
        }
    }
}
