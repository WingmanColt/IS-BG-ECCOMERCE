using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    }

    public static class DataSeeder
    {
        public static void Seed(ApplicationDbContext db)
        {
            if (!db.Categories.Any())
            {
                var cat1 = new Category { Name = "Електроника" };
                var cat2 = new Category { Name = "Облекло" };
                db.Categories.AddRange(cat1, cat2);
                db.SaveChanges();

                db.Products.AddRange(
                    new Product { Name = "Слушалки", Price = 59.99m, CategoryId = cat1.Id, Stock = 50, ImageUrl = "https://tctechcrunch2011.files.wordpress.com/2014/11/solo2-wireless-red-quarter.jpg?w=738" },
                    new Product { Name = "Iphone X", Price = 1999.99m, CategoryId = cat1.Id, Stock = 100, ImageUrl = "https://www.bell.ca/Styles/wireless/all_languages/all_regions/catalog_images/large/iPhoneX_spgry-en_lrg.png" },
                    new Product { Name = "Обувки", Price = 29.99m, CategoryId = cat2.Id, Stock = 100, ImageUrl = "https://i.pinimg.com/736x/05/58/c7/0558c796ee706b5cb289ffb68e3b509c--is-the-best-to-the.jpg" },
                    new Product { Name = "Камера", Price = 119.99m, CategoryId = cat1.Id, Stock = 100, ImageUrl = "https://www.grootgadgets.com/wp-content/uploads/2017/03/Canon-70-200mm-Lens-mug-White-replica-groot-gadgets-1-400x400.jpg" }

                );
                db.SaveChanges();
            }
        }
    }
}

