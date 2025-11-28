using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Името е задължително")]
        public string? CustomerName { get; set; }

        [Required(ErrorMessage = "Телефонът е задължителен")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Адресът е задължителен")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Имейлът е задължителен")]
        [EmailAddress(ErrorMessage = "Невалиден имейл")]
        public string? Email { get; set; }

        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
