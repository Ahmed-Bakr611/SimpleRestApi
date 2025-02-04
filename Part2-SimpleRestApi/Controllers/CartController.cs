using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Part2_SimpleRestApi.Models;
using Part2_SimpleRestApi.Services;

namespace Part2_SimpleRestApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [Authorize]

    public class CartController : ControllerBase
    {
        private readonly IFakeStoreService _fakeStoreService;

        public CartController(IFakeStoreService fakeStoreService)
        {
            _fakeStoreService = fakeStoreService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCart(CartRequest cartRequest)
        {
            var cart = await _fakeStoreService.CreateCartAsync(cartRequest);
            return Ok(cart);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCart(int id, CartRequest cartRequest)
        {
       
            var cart = await _fakeStoreService.UpdateCartAsync(id, cartRequest);
            return Ok(cart);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchCart(int id, CartRequest cartRequest)
        {
            var cart = await _fakeStoreService.PatchCartAsync(id, cartRequest);
            return Ok(cart);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            var cart = await _fakeStoreService.DeleteCartAsync(id);
            return Ok(cart);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCartById(int id)
        {
            var cart = await _fakeStoreService.GetCartByIdAsync(id);
            return Ok(cart);
        }

    }
}
