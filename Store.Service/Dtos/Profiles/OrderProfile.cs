using AutoMapper;
using Store.Data.Entities.Order;
using Store.Service.Dtos.OrderDtos;
using Store.Service.Services.OrderService;
using Store.Service.Services.ProductServices;

namespace Store.Service.Dtos.Profiles
{
	public class OrderProfile :Profile
	{ 
		public OrderProfile()
		{
			CreateMap<ShippingAddress , ShippingAddressDto>().ReverseMap();

			CreateMap<Order, OrderDetailsDto>()
					.ForMember(dest => dest.DeliveryMethodName, options => options.MapFrom(src => src.DeliveryMethod.ShortName))
					.ForMember(dest => dest.ShippingPrice, options => options.MapFrom(src => src.DeliveryMethod.Price));

			CreateMap<OrderItems, OrderItemDto>()
					.ForMember(dest => dest.ProductItemId, options => options.MapFrom(src => src.ProductItem.ProductId))
					.ForMember(dest => dest.ProductName, options => options.MapFrom(src => src.ProductItem.ProductName))
					.ForMember(dest => dest.PictureUrl, options => options.MapFrom<OrderPictureUrlResolver>());
					


		}
	}
}
