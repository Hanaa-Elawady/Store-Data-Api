using AutoMapper;
using Store.Repository.Basket;
using Store.Repository.Basket.Models;
using Store.Service.Dtos.BasketDtos;
using Store.Service.Interfaces;

namespace Store.Service.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteBasketAsync(string Id)
        => await _basketRepository.DeleteBasketAsync(Id);

        public async Task<CustomerBasketDto> GetBasketAsync(string Id)
        {
            var basket = await _basketRepository.GetBasketAsync(Id);

            if (basket is null)
                return new CustomerBasketDto();

            var mappedBasket = _mapper.Map<CustomerBasketDto>(basket);
            return mappedBasket;

        }

        public async Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto basket)
        {
            if (basket.Id is null)
            {
                basket.Id = GenerateRandomBasketId();
            }

            var customerBasket = _mapper.Map<CustomerBasket>(basket);
            var UpdatesBasket = await _basketRepository.UpdateBasketAsync(customerBasket);
            var mappedUpdatedBasket = _mapper.Map<CustomerBasketDto>(UpdatesBasket);

            return mappedUpdatedBasket;
        }

        private string GenerateRandomBasketId()
        {
            Random random = new Random();
            var number = random.Next(1000, 10000);
            return $"BS_{number}";
        }

    }
}
