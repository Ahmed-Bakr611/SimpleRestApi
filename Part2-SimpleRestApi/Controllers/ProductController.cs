using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Part2_SimpleRestApi.Services;

namespace Part2_SimpleRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IFakeStoreService _fakeStoreService;

        public ProductController(IFakeStoreService fakeStoreService)
        {
            _fakeStoreService = fakeStoreService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(
            [FromQuery] int limit = 5,
            [FromQuery] string? category = null,
            [FromQuery] string? name = null,
            [FromQuery] decimal? minPrice = null,
            [FromQuery] decimal? maxPrice = null
            )
            
        {
            var products = await _fakeStoreService.GetProductsAsync(limit);
            

            // Apply filters
            if (!string.IsNullOrEmpty(category))
            {
                products = products.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(name))
            {
                products = products.Where(p => p.Title.Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            if (minPrice.HasValue && minPrice>0)
            {
                products = products.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue && maxPrice>0)
            {
                products = products.Where(p => p.Price <= maxPrice.Value);
            }

        
            return Ok(products);
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _fakeStoreService.GetProductByIdAsync(id);
            return Ok(product);
        }

    }
}
