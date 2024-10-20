using Store.Data.Entities.Order.Enums;

namespace Store.Data.Entities.Order
{
    public class Order :BaseEntity<Guid>
    {
        public string CustomerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public ShippingAddress ShippingAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public int? DeliveryMethodId { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Placed;
        public OrderPaymentStatus OrderPaymentStatus { get; set; } = OrderPaymentStatus.Pending;
        public List<OrderItems> OrderItems { get; set; }
        public string? BasketId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal GetTotal()
            => SubTotal+ DeliveryMethod.Price ;

        public string? PaymentIntentId { get; set; }
          
    }
}
