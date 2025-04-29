using Ecommerce.Model;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.OrderService.Data
{
	public class OrderDbContext: DbContext
	{
		public DbSet<OrderModel> Orders { get; set; }

		public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
		{
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<OrderModel>().ToTable("Orders");
			modelBuilder.Entity<OrderModel>().HasKey(o => o.Id);
			modelBuilder.Entity<OrderModel>().Property(o => o.CustomerName).IsRequired().HasMaxLength(128);
			modelBuilder.Entity<OrderModel>().Property(o => o.ProductId).IsRequired();
			modelBuilder.Entity<OrderModel>().Property(o => o.ProductName).IsRequired().HasMaxLength(128);
			modelBuilder.Entity<OrderModel>().Property(o => o.Price).IsRequired();
			modelBuilder.Entity<OrderModel>().Property(o => o.OrderDate).IsRequired();
			base.OnModelCreating(modelBuilder);
		}
	}
}
