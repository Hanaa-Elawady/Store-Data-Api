using Store.Repository.Basket.Models;

namespace Store.Repository.Basket
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(string Id);
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(string Id);
    }
}
