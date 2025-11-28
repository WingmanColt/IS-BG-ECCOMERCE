using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Areas.Identity.Pages.Cart
{
    public class Cart_IndexModel : PageModel
    {
        private readonly ICartService _cart;

        public Cart_IndexModel(ICartService cart)
        {
            _cart = cart;
        }


        public List<CartItem> Items { get; set; } = new();
        public decimal TotalPrice { get; set; }

        public async Task OnGetAsync()
        {
            Items = await _cart.GetItemsAsync();
            TotalPrice = await _cart.GetTotalAsync();
        }


        public async Task<JsonResult> OnGetCountAsync()
        {
            var items = await _cart.GetItemsAsync();
            return new JsonResult(items.Count);
        }

        public async Task<JsonResult> OnGetItemsPreviewAsync()
        {
            var items = await _cart.GetItemsAsync();
            var preview = items.Take(3).Select(i => new { i.ProductName, i.Quantity, i.LineTotal });
            return new JsonResult(preview);
        }
    }
}
