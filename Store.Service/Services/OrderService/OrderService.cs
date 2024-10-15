using AutoMapper;
using Store.Data.Entities;
using Store.Data.Entities.Order;
using Store.Repository.Interfaces;
using Store.Repository.Specification.OrderSpecs;
using Store.Service.Dtos.OrderDtos;
using Store.Service.Interfaces;

namespace Store.Service.Services.OrderService
{
	public class OrderService : IOrderService
	{
		private readonly IBasketService _basketService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IPaymentService _paymentService;

		public OrderService(IBasketService basketService, IUnitOfWork unitOfWork, IMapper mapper, IPaymentService paymentService)
		{
			_basketService = basketService;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_paymentService = paymentService;
		}

		public async Task<OrderDetailsDto> createOrderAsync(OrderDto input)
		{
			var Basket = await _basketService.GetBasketAsync(input.BasketId);
			if (Basket == null)
				throw new Exception("Basket Not Found");

			var orderItems = new List<OrderItemDto>();

			#region Get the real Data (orderItem) from Database 
			foreach (var item in Basket.basketItems)
			{
				var Product = await _unitOfWork.Repository<Product , int>().GetByIdAsync(item.ProductId);
				if (Product == null)
					throw new Exception($"Product {item.ProductId} Not Found");

				var ProductOrdered = new OrderItems
				{
					Price = Product.Price,
					Quantity = item.Quantity,
					ProductItem = new ProductItem
					{
						PictureUrl = Product.PictureUrl,
						ProductId = Product.Id,
						ProductName = Product.Name,
					}
				};

				var MappedProductOrdered = _mapper.Map<OrderItemDto>(ProductOrdered);

				orderItems.Add(MappedProductOrdered);
			}
			#endregion

			var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetByIdAsync(input.DeliveryMethodId.Value);
			if (deliveryMethod == null)
				throw new Exception("No deliveryMethod");

			var subTotalPrice =  orderItems.Sum(x => x.Price * x.Quantity);

			#region Payment

			var specs = new OrderWithPaymentIntentSpecs(Basket.PaymentIntentId);
			var ExistingOrder = await _unitOfWork.Repository<Order, Guid>().GetWithSpecsByIdAsync(specs);

			if(ExistingOrder == null)
			{
				await _paymentService.CreateOrUpdatePaymentIntent(Basket);
			}
			#endregion
			var mappedshippingAddress = _mapper.Map<ShippingAddress>(input.ShippingAddress);
			var MappedOrderItems = _mapper.Map<List<OrderItems>>(orderItems);
			var order = new Order
			{
				DeliveryMethodId = deliveryMethod.Id,
				ShippingAddress = mappedshippingAddress,
				CustomerEmail = input.CustomerEmail,
				BasketId = input.BasketId,
				SubTotal = subTotalPrice,
				OrderItems = MappedOrderItems,
				PaymentIntentId = Basket.PaymentIntentId
			};

			await _unitOfWork.Repository<Order , Guid >().AddAsync(order);
			await _unitOfWork.CompleteAsync();

			return _mapper.Map<OrderDetailsDto>(order);
		}

		public async Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodsAsync()
			=> await _unitOfWork.Repository<DeliveryMethod, int>().GetAllAsync(); 
	

		public async Task<IReadOnlyList<OrderDetailsDto>> GetAllOrdersForUserAsync(string CustomerEmail)
		{
			var specs = new OrderWithItemSpecs(CustomerEmail);
			var orders = await  _unitOfWork.Repository<Order, Guid>().GetWithSpecsAllAsync(specs);
			if (!orders.Any())
				throw new Exception("No Orders Yet");

			var mappedOrders = _mapper.Map<List<OrderDetailsDto>>(orders);
			return mappedOrders;
		}

		public async Task<OrderDetailsDto> GetOrderByIdAsync(Guid Id)
		{
			var specs = new OrderWithItemSpecs(Id);
			var order = await _unitOfWork.Repository<Order, Guid>().GetWithSpecsByIdAsync(specs);
			if (order is null)
				throw new Exception("No Order With this Id ");

			var mappedOrder = _mapper.Map<OrderDetailsDto>(order);
			return mappedOrder;
		}
	}
}
