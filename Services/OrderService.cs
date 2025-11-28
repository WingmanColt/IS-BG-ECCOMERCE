using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(Order order, IEnumerable<CartItem> cartItems);
    }

    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICartService _cartService;

        public OrderService(ApplicationDbContext context, ICartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }

        public async Task<Order> CreateOrderAsync(Order order, IEnumerable<CartItem> cartItems)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));
            if (cartItems == null || !cartItems.Any()) throw new ArgumentException("Cart is empty", nameof(cartItems));

            // Map cart items to order items
            foreach (var cartItem in cartItems)
            {
                order.Items.Add(new OrderItem
                {
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    UnitPrice = cartItem.UnitPrice
                });
            }

            // Calculate total
            order.TotalAmount = order.Items.Sum(i => i.LineTotal);

            // Save order
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Clear cart
            foreach (var cartItem in cartItems)
            {
                await _cartService.RemoveAsync(cartItem.ProductId);
            }

            return order;
        }
    }
}
