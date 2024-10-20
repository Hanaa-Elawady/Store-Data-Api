using Store.Data.Entities.Order.Enums;

namespace Store.Service.Dtos.OrderDtos
{
	public class OrderDetailsDto
	{
		public Guid Id { get; set; }
		public string CustomerEmail { get; set; }
		public DateTimeOffset OrderDate { get; set; }
		public ShippingAddressDto ShippingAddress { get; set; }
		public string DeliveryMethodName { get; set; }
		public OrderPaymentStatus OrderPaymentStatus { get; set; } = OrderPaymentStatus.Pending;
		public OrderStatus OrderStatus { get; set; }
		public IReadOnlyList<OrderItemDto> OrderItems { get; set; }
		public decimal SubTotal { get; set; }
		public decimal ShippingPrice { get; set; }
		public decimal Total { get; set; }
		public string? BasketId { get; set; }
		public string? PaymentIntentId { get; set; }


	}
}
