namespace Ecommerce.Model
{
	public class OrderModel
	{
		public int Id { get; set; }
		public string? CustomerName { get; set; }
		public int ProductId { get; set; }
		public string? ProductName { get; set; }
		public decimal Price { get; set; }
		public DateTime OrderDate { get; set; }
	}
}
