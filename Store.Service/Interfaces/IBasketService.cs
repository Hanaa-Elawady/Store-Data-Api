using Store.Service.Dtos.BasketDtos;

namespace Store.Service.Interfaces
{
    public interface IBasketService
    {
        Task<CustomerBasketDto> GetBasketAsync(string Id);
        Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto basket);
        Task<bool> DeleteBasketAsync(string Id);
    }
}
