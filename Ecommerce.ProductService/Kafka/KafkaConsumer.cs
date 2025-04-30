
using Confluent.Kafka;
using Ecommerce.Model;
using Ecommerce.ProductService.Data;
using System.Text.Json;

namespace Ecommerce.ProductService.Kafka
{
	public class KafkaConsumer(IServiceScopeFactory scopeFactory) : BackgroundService
	{
		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			return Task.Run(() => ConsumeAsync("order-topic", stoppingToken), stoppingToken);
		}

		public async Task ConsumeAsync(string topic, CancellationToken stoppingToken)
		{
			var config = new ConsumerConfig
			{
				GroupId = "order-group",
				BootstrapServers = "localhost:9092",
				AutoOffsetReset = AutoOffsetReset.Earliest,
			};

			using var consumer = new ConsumerBuilder<string, string>(config).Build();
			consumer.Subscribe(topic);

			while (!stoppingToken.IsCancellationRequested)
			{
				var consumeResult = consumer.Consume(stoppingToken);

				var order = JsonSerializer.Deserialize<OrderModel>(consumeResult.Message.Value);

				if (order == null)
					continue;

				using var scope = scopeFactory.CreateScope();
				var dbContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
				
				var product = dbContext.Products.Find(order?.ProductId);
				if (product != null) {
					product.Stock -= order.Quantity;
					await dbContext.SaveChangesAsync();
				}				
			}

			consumer.Close();
		}
	}
}
