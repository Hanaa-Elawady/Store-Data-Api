using Store.Data.Entities;
using Store.Service.Dtos.OrderDtos;

namespace Store.Service.Interfaces
{
	public interface IOrderService
	{
		Task<OrderDetailsDto> createOrderAsync(OrderDto input);
		Task<IReadOnlyList<OrderDetailsDto>> GetAllOrdersForUserAsync(string CustomerEmail);
		Task<OrderDetailsDto> GetOrderByIdAsync(Guid Id);
		Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodsAsync();
	}
}
