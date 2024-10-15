using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Data.Entities;
using Store.Service.Dtos.OrderDtos;
using Store.Service.HandleResponse;
using Store.Service.Interfaces;
using System.Security.Claims;

namespace Store.Web.Controllers
{
	[Authorize]
	public class OrdersController : BaseController
	{
		private readonly IOrderService _orderService;

		public OrdersController(IOrderService orderService)
		{
			_orderService = orderService;
		}

		[HttpPost]
		public async Task<ActionResult<OrderDetailsDto>> CreateOrderAsync(OrderDto input)
		{
			var order = await _orderService.createOrderAsync(input);
			if (order == null)
				return BadRequest(new Response(400, "Cannot Ceate the Order "));

			return Ok(order);
		}

		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<OrderDetailsDto>>> GetAllOrdersForUserAsync()
		{
			var Email = User.FindFirstValue("email");
			if (Email == null)
				return BadRequest(new Response(400, "User Not Found"));

			var Orders = await _orderService.GetAllOrdersForUserAsync(Email);
			if (Orders == null)
				return BadRequest(new Response(400, "No Orders Found For This User"));

			return Ok(Orders);
		}
		[HttpGet]
		public async Task<ActionResult<OrderDetailsDto>> GetOrderByIdAsync(Guid Id)
		{
			var Email = User.FindFirstValue("email");
			if (Email == null)
				return BadRequest(new Response(400, "User Not Found"));

			var Order = await _orderService.GetOrderByIdAsync(Id);
			if (Order == null)
				return BadRequest(new Response(400, "No Orders Found For This User"));

			return Ok(Order);
		}
		[HttpGet]
		public async Task<ActionResult<DeliveryMethod>> GetAllDeliveryMethodsAsync()
		=> Ok(await _orderService.GetAllDeliveryMethodsAsync());
	}
}
