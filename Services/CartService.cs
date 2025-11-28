using Microsoft.AspNetCore.Http;
using System.Text.Json;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface ICartService
    {
        Task AddAsync(Product product);
        Task ClearAsync();
        Task<List<CartItem>> GetItemsAsync();
        Task<decimal> GetTotalAsync();
        Task RemoveAsync(int productId);
    }

    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _http;
        private const string CartKey = "CART_DATA";

        public CartService(IHttpContextAccessor http) => _http = http;

        private async Task<List<CartItem>> LoadAsync()
        {
            return await Task.Run(() =>
            {
                var json = _http.HttpContext.Session.GetString(CartKey);
                return json == null ? new List<CartItem>() : JsonSerializer.Deserialize<List<CartItem>>(json)!;
            });
        }

        private async Task SaveAsync(List<CartItem> items)
        {
            await Task.Run(() =>
            {
                var json = JsonSerializer.Serialize(items);
                _http.HttpContext.Session.SetString(CartKey, json);
            });
        }

        public async Task<List<CartItem>> GetItemsAsync() => await LoadAsync();

        public async Task AddAsync(Product product)
        {
            var items = await LoadAsync();
            var existing = items.FirstOrDefault(i => i.ProductId == product.Id);

            if (existing == null)
                items.Add(new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name ?? "",
                    UnitPrice = product.Price,
                    ImageUrl = product.ImageUrl ?? "",
                    Quantity = 1
                });
            else
                existing.Quantity++;

            await SaveAsync(items);
        }

        public async Task RemoveAsync(int productId)
        {
            var items = await LoadAsync();
            var item = items.FirstOrDefault(i => i.ProductId == productId);

            if (item != null)
            {
                items.Remove(item);
                await SaveAsync(items);
            }
        }

        public async Task ClearAsync()
        {
            await SaveAsync(new List<CartItem>());
        }

        public async Task<decimal> GetTotalAsync()
        {
            var items = await LoadAsync();
            return items.Sum(i => i.LineTotal);
        }
    }

}
