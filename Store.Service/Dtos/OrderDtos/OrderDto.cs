namespace Store.Service.Dtos.OrderDtos
{
	public class OrderDto
	{
		public string CustomerEmail { get; set; }
		public ShippingAddressDto ShippingAddress { get; set; }
		public int? DeliveryMethodId { get; set; }
		public string BasketId { get; set; }
	}
}
