using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Part2_SimpleRestApi.Services;

namespace Part2_SimpleRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly IFakeStoreService _fakeStoreService;

        public CategoryController(IFakeStoreService fakeStoreService)
        {
            _fakeStoreService = fakeStoreService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _fakeStoreService.GetCategoriesAsync();
            return Ok(categories);
        }
    }
}
