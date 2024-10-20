using StackExchange.Redis;
using Store.Repository.Basket.Models;
using System.Text.Json;

namespace Store.Repository.Basket
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string Id)
            => await _database.KeyDeleteAsync(Id);

        public async Task<CustomerBasket> GetBasketAsync(string Id)
        {
            var basket = await _database.StringGetAsync(Id);

            return basket.IsNullOrEmpty ? null :JsonSerializer.Deserialize<CustomerBasket>(basket) ;
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var IsCreated = await _database.StringSetAsync(basket.Id , JsonSerializer.Serialize(basket),TimeSpan.FromMinutes(30));

            if (!IsCreated)
                return null;

            return await GetBasketAsync(basket.Id);
        }
    }
}
