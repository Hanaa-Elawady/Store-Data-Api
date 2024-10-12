using Store.Service.Dtos.BasketDtos;
using Store.Service.Dtos.OrderDtos;

namespace Store.Service.Interfaces
{
	public interface IPaymentService
	{
		Task<CustomerBasketDto> CreateOrUpdatePaymentIntent(CustomerBasketDto input);
		Task<OrderDetailsDto> UpdateOrderPaymentSucceeded(string paymentIntendId);
		Task<OrderDetailsDto> UpdateOrderPaymentFailed(string paymentIntendId);   

	}
}
