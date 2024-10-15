using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.Data.Entities;
using Store.Data.Entities.Order;
using Store.Data.Entities.Order.Enums;
using Store.Repository.Interfaces;
using Store.Repository.Specification.OrderSpecs;
using Store.Service.Dtos.BasketDtos;
using Store.Service.Dtos.OrderDtos;
using Store.Service.Interfaces;
using Stripe;
using Product = Store.Data.Entities.Product;

namespace Store.Service.Services
{
	public class PaymentService : IPaymentService
	{
		private readonly IConfiguration _configuration;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IBasketService _basketService;
		private readonly IMapper _mapper;

		public PaymentService(IConfiguration configuration , IUnitOfWork unitOfWork, IBasketService basketService,IMapper mapper)
		{
			_configuration = configuration;
			_unitOfWork = unitOfWork;
			_basketService = basketService;
			_mapper = mapper;
		}

		public async Task<CustomerBasketDto> CreateOrUpdatePaymentIntent(CustomerBasketDto input)
		{
			StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

			if (input == null)
				throw new Exception("Basket Is Empty");

			var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetByIdAsync(input.DeliveryMethodId);

			if (deliveryMethod == null)
				throw new Exception("No deliveryMethod");

			decimal shippingPrice = deliveryMethod.Price;

			foreach (var item in input.basketItems)
			{
				var product = await _unitOfWork.Repository<Product, int>().GetByIdAsync(item.ProductId);
				if (item.Price != product.Price)
					item.Price = product.Price;
			}

			var service = new PaymentIntentService();

			PaymentIntent paymentIntent;
			if (string.IsNullOrEmpty(input.PaymentIntentId))
			{
				var options = new PaymentIntentCreateOptions
				{
					Amount = (long)input.basketItems.Sum(item => (item.Quantity * (item.Price *100))+ (long)(shippingPrice * 100)),
					Currency = "USD",
					PaymentMethodTypes = new List<string> { "card"}

				};
				paymentIntent = await service.CreateAsync(options);
				input.PaymentIntentId = paymentIntent.Id;
				input.ClientSecret = paymentIntent.ClientSecret;
			}
			else
			{
				var options = new PaymentIntentUpdateOptions
				{
					Amount = (long)input.basketItems.Sum(item => (item.Quantity * (item.Price * 100)) + (long)(shippingPrice * 100)),
				};
				await service.UpdateAsync(input.PaymentIntentId, options);
			}

			await _basketService.UpdateBasketAsync(input);

			return input;
		}

		public async Task<OrderDetailsDto> UpdateOrderPaymentFailed(string paymentIntendId)
		{
			var specs = new OrderWithPaymentIntentSpecs(paymentIntendId);
			var order = await _unitOfWork.Repository<Order, Guid>().GetWithSpecsByIdAsync(specs);
			if (order == null)
				throw new Exception("Order Not Exist");

			order.OrderPaymentStatus = OrderPaymentStatus.Failed;
			_unitOfWork.Repository<Order , Guid>().Update(order);
			await _unitOfWork.CompleteAsync(); 
			var mappedOrder =  _mapper.Map<OrderDetailsDto>(order);
			return mappedOrder;
		}

		public async Task<OrderDetailsDto> UpdateOrderPaymentSucceeded(string paymentIntendId)
		{
			var specs = new OrderWithPaymentIntentSpecs(paymentIntendId);
			var order = await _unitOfWork.Repository<Order, Guid>().GetWithSpecsByIdAsync(specs);
			if (order == null)
				throw new Exception("Order Not Exist");

			order.OrderPaymentStatus = OrderPaymentStatus.Received;
			_unitOfWork.Repository<Order, Guid>().Update(order);
			await _unitOfWork.CompleteAsync();
			await _basketService.DeleteBasketAsync(order.BasketId);
			var mappedOrder = _mapper.Map<OrderDetailsDto>(order);
			return mappedOrder;
		}
	}
}
