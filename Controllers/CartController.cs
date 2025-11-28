using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Services;
using WebApplication1.ViewModels;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _cart;
    private readonly ApplicationDbContext _db;

    public CartController(ICartService cart, ApplicationDbContext db)
    {
        _cart = cart;
        _db = db;
    }

    [HttpGet("Count")]
    public async Task<IActionResult> GetCount()
    {
        var items = await _cart.GetItemsAsync();
        var count = items.Sum(i => i.Quantity);
        return Ok(count);
    }

    [HttpGet("ItemsPreview")]
    public async Task<IActionResult> GetItemsPreview()
    {
        var items = await _cart.GetItemsAsync();
        var preview = items.Select(i => new CartItemDto
        {
            productId = i.ProductId,
            productName = i.ProductName,
            quantity = i.Quantity,
            unitPrice = i.UnitPrice,
            lineTotal = i.UnitPrice * i.Quantity,
            imageUrl = i.ImageUrl 
        }).ToList();

        return Ok(preview);
    }


    [HttpPost("Add")]
    public async Task<IActionResult> Add([FromBody] AddToCartRequest request)
    {
        var product = await _db.Products.FindAsync(request.ProductId);
        if (product == null) return NotFound();

        await _cart.AddAsync(product);
        return Ok(new { productName = product.Name });
    }

    [HttpPost("Remove")]
    public async Task<IActionResult> Remove([FromBody] RemoveFromCartRequest request)
    {
        if (request == null || request.ProductId <= 0)
            return BadRequest("Invalid product ID");

        await _cart.RemoveAsync(request.ProductId); 

        return Ok(new { productId = request.ProductId });
    }
}
