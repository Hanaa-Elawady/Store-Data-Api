namespace Store.Service.Dtos.OrderDtos
{
	public class OrderItemDto
	{
		public decimal Price { get; set; }

		public int Quantity { get; set; }
		public int ProductItemId { get; set; }
		public string ProductName { get; set; }
		public string PictureUrl { get; set; }
		public Guid OrderId { get; set; }
	}
}
