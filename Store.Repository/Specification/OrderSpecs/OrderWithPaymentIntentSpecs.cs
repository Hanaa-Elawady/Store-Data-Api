using Store.Data.Entities.Order;

namespace Store.Repository.Specification.OrderSpecs
{
	public class OrderWithPaymentIntentSpecs : BaseSpecification<Order>
	{
		public OrderWithPaymentIntentSpecs(string PaymentIntentId) 
			: base(order=> order.PaymentIntentId == PaymentIntentId)
		{
		}
	}
}
