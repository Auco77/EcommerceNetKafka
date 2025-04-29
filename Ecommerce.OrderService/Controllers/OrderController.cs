using Ecommerce.Model;
using Ecommerce.OrderService.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.OrderService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController(OrderDbContext dbContext) : ControllerBase
	{
		[HttpGet]
		public async Task<List<OrderModel>> GetOrders() => await dbContext.Orders.ToListAsync();

		[HttpGet("{id:int}")]
		public async Task<OrderModel?> GetOrderAsync(int id) => await dbContext.Orders.FindAsync(id);

		[HttpPost]
		public async Task<IActionResult> CreateOrder(OrderModel order)
		{
			order.OrderDate = DateTime.Now;
			dbContext.Orders.Add(order);
			await dbContext.SaveChangesAsync();
			return CreatedAtAction(nameof(GetOrderAsync), new { id = order.Id }, order);
		}
	}
}
