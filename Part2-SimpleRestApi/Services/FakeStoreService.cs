using Part2_SimpleRestApi.Models;
using System.Text;
using System.Text.Json;

namespace Part2_SimpleRestApi.Services
{
    public interface IFakeStoreService
    {


        //product endpoints
        Task<IEnumerable<Product>> GetProductsAsync(int limit);
        Task<Product> GetProductByIdAsync(int id);


        //cart endpoints
        Task<CartResponse> CreateCartAsync(CartRequest cartRequest);
        Task<CartResponse> UpdateCartAsync(int id, CartRequest cartRequest);
        Task<CartResponse> PatchCartAsync(int id, CartRequest cartRequest);
        Task<CartResponse> DeleteCartAsync(int id);
        Task<CartResponse> GetCartByIdAsync(int id);
        
        //categories endpoints
        Task<IEnumerable<string>> GetCategoriesAsync();

    }

    public class FakeStoreService:IFakeStoreService
    {
        private readonly HttpClient _httpClient;

        public FakeStoreService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            // Add your actual authorization header
           // httpClient.hea.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "your_token_here");
        }

        public async Task<CartResponse> CreateCartAsync(CartRequest cartRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("https://fakestoreapi.com/carts", cartRequest);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CartResponse>();
        }

        public async Task<CartResponse> UpdateCartAsync(int id, CartRequest cartRequest)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://fakestoreapi.com/carts/{id}", cartRequest);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CartResponse>();
        }

        public async Task<CartResponse> PatchCartAsync(int id, CartRequest cartRequest)
        {
            var response = await _httpClient.PatchAsync($"https://fakestoreapi.com/carts/{id}", new StringContent(
                JsonSerializer.Serialize(cartRequest), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CartResponse>();
        }

        public async Task<CartResponse> DeleteCartAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://fakestoreapi.com/carts/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CartResponse>();
        }

        public async Task<CartResponse> GetCartByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"https://fakestoreapi.com/carts/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CartResponse>();
        }

        public async Task<IEnumerable<string>> GetCategoriesAsync()
        {
            var response = await _httpClient.GetAsync("https://fakestoreapi.com/products/categories");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<string>>();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(int limit)
        {
            var response = await _httpClient.GetAsync($"https://fakestoreapi.com/products?limit={limit}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<Product>>();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"https://fakestoreapi.com/products/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Product>();
        }
    }
}
