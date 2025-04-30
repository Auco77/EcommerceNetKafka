using Confluent.Kafka;
using Ecommerce.Model;
using Ecommerce.OrderService.Data;
using Ecommerce.OrderService.Kafka;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Ecommerce.OrderService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController(OrderDbContext dbContext, IKafkaProducer kafkaProducer) : ControllerBase
	{
		[HttpGet]
		public async Task<List<OrderModel>> GetOrders() => await dbContext.Orders.ToListAsync();

		[HttpGet("{id:int}")]
		public async Task<OrderModel?> GetOrderAsync(int id) => await dbContext.Orders.FindAsync(id);

		[HttpPost]
		public async Task<OrderModel> CreateOrder(OrderModel order)
		{
			order.OrderDate = DateTime.Now;
			dbContext.Orders.Add(order);
			await dbContext.SaveChangesAsync();

			//produce message to Kafka topic
			await kafkaProducer.ProducerAsync("order-topic", new Message<string, string>
			{
				Key = order.Id.ToString(),
				Value = JsonSerializer.Serialize(order)
			});

			return order;
		}
	}
}
