using Store.Data.Entities.Order;

namespace Store.Repository.Specification.OrderSpecs
{
	public class OrderWithItemSpecs : BaseSpecification<Order>
	{
		public OrderWithItemSpecs(string CustomerEmail) 
			: base(order => order.CustomerEmail== CustomerEmail)
		{
			AddInclude(order => order.DeliveryMethod);
			AddInclude(order => order.OrderItems);
			AddOrderByDescending(order => order.OrderDate);

		}

		public OrderWithItemSpecs(Guid Id )
		: base(order => order.Id == Id)
		{
			AddInclude(order => order.DeliveryMethod);
			AddInclude(order => order.OrderItems);
		}
	}
}
