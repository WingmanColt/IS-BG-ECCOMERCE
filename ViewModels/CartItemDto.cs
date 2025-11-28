namespace WebApplication1.ViewModels
{
    public class CartItemDto
    {
        public int productId { get; set; }
        public string productName { get; set; } = "";
        public int quantity { get; set; }
        public decimal unitPrice { get; set; }
        public decimal lineTotal { get; set; }
        public string imageUrl { get; set; } = "";
    }
}
