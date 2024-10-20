
using StackExchange.Redis;
using Store.Service.Interfaces;
using System.Text.Json;

namespace Store.Service.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _database;

        public CacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<string> GetCacheResponceAsync(string key)
        {
            var cachedResponse = await _database.StringGetAsync(key);

            if (cachedResponse.IsNullOrEmpty)
                return null;

            return cachedResponse.ToString();
        }

        public async Task setCacheResponceAsync(string key, object response, TimeSpan timeToLive)
        {
            if (response is null)
                return;

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            var serializedResponse = JsonSerializer.Serialize(response, options);

            await _database.StringSetAsync(key, serializedResponse, timeToLive);
        }

    }
}
