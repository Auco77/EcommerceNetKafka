using Ecommerce.Model;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.ProductService.Data
{
	public class ProductDbContext : DbContext
	{
		public DbSet<ProductModel> Products { get; set; }

		public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
		{
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ProductModel>().ToTable("Products");
			modelBuilder.Entity<ProductModel>().HasKey(p => p.Id);
			modelBuilder.Entity<ProductModel>().Property(p => p.Name).IsRequired().HasMaxLength(128);
			modelBuilder.Entity<ProductModel>().Property(p => p.Price).IsRequired();
			modelBuilder.Entity<ProductModel>().Property(p => p.Stock).IsRequired();

			var seeds = new List<ProductModel>{
				new ProductModel{Id = 1, Name="Shirt", Stock = 20, Price = 10},
				new ProductModel{Id = 2, Name="Pant", Stock = 50, Price = 30},
				new ProductModel{Id = 3, Name="Polo", Stock = 100, Price = 40},
				new ProductModel{Id = 4, Name="Shoes", Stock = 200, Price = 35},
				new ProductModel{Id = 5, Name="Bear", Stock = 1000, Price = 10},
			};

			modelBuilder.Entity<ProductModel>().HasData(seeds);

			base.OnModelCreating(modelBuilder);
		}

	}
}
