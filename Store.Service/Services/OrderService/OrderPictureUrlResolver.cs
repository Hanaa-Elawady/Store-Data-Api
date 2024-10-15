using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.Data.Entities.Order;
using Store.Service.Dtos.OrderDtos;

namespace Store.Service.Services.OrderService
{
	public class OrderPictureUrlResolver : IValueResolver<OrderItems , OrderItemDto ,string>
	{
		private readonly IConfiguration _configuration;

		public OrderPictureUrlResolver(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string Resolve(OrderItems source, OrderItemDto destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.ProductItem.PictureUrl) ||!source.ProductItem.PictureUrl.Contains(_configuration["BaseUrl"]))
				return $"{_configuration["BaseUrl"]}/{source.ProductItem.PictureUrl}";

			return null;
		}
	}
}
