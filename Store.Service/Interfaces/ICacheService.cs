namespace Store.Service.Interfaces
{
    public interface ICacheService
    {
        Task setCacheResponceAsync(string key, object response, TimeSpan timeToLive);
        Task<string> GetCacheResponceAsync(string key);
    }
}
