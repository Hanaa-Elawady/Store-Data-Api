using AutoMapper;
using Store.Repository.Basket.Models;
using Store.Service.Dtos.BasketDtos;

namespace Store.Service.Dtos.Profiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket,CustomerBasketDto>().ReverseMap(); 
            CreateMap<BasketItem,BasketItemDto>().ReverseMap(); 

        }
    }
}
