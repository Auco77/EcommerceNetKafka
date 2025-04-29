using Ecommerce.Model;
using Ecommerce.ProductService.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.ProductService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController(ProductDbContext dbContext) : ControllerBase
	{
		[HttpGet]
		public async Task<List<ProductModel>> GetProducts() => await dbContext.Products.ToListAsync();

		[HttpGet("{id:int}")]
		public async Task<ProductModel?> GetProductAsync(int id) => await dbContext.Products.FindAsync(id);
	}
}