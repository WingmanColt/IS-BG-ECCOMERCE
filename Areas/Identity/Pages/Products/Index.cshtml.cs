using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Areas.Identity.Pages.Products
{
    public class ProductsIndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public ProductsIndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IList<Product> Products { get; set; } = new List<Product>();
        public IList<Category> Categories { get; set; } = new List<Category>();

        [BindProperty(SupportsGet = true)]
        public int? CategoryId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 9;
        public int Total { get; set; }

        public async Task OnGetAsync()
        {
            var query = _db.Products.Include(p => p.Category).AsQueryable();
            if (CategoryId.HasValue) query = query.Where(p => p.CategoryId == CategoryId.Value);

            Total = await query.CountAsync();
            Products = await query.OrderBy(p => p.Name)
                                  .Skip((Page - 1) * PageSize)
                                  .Take(PageSize)
                                  .ToListAsync();

            Categories = await _db.Categories.ToListAsync();
        }

    }
}
