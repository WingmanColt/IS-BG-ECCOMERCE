using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Areas.Identity.Pages.Checkout
{
    public class Checkout_IndexModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;

        public Checkout_IndexModel(IOrderService orderService, ICartService cartService)
        {
            _orderService = orderService;
            _cartService = cartService;
        }

        [BindProperty]
        public Order Order { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var cartItems = await _cartService.GetItemsAsync();
            if (!cartItems.Any())
            {
                ModelState.AddModelError(string.Empty, "Your cart is empty.");
                return Page();
            }

            await _orderService.CreateOrderAsync(Order, cartItems);

            return RedirectToPage("Success");
        }
    }
}
