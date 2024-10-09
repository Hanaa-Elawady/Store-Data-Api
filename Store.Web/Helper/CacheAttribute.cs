using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Store.Service.Interfaces;
using System.Text;

namespace Store.Web.Helper
{
    public class CacheAttribute :Attribute ,IAsyncActionFilter
    {
        private readonly int _timeToLiveInSec;

        public CacheAttribute(int timeToLiveInSec)
        {
            _timeToLiveInSec = timeToLiveInSec;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var _cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
            var cacheResponse = await _cacheService.GetCacheResponceAsync(cacheKey);
            if (!string.IsNullOrEmpty(cacheResponse))
            {
                var contentResult = new ContentResult
                {
                    Content = cacheResponse,
                    ContentType = "application/json",
                    StatusCode = 200,
                };

                context.Result = contentResult;

                return;
            }
            else
            {
                var executedContext = await next();
                if (executedContext.Result is OkObjectResult response)
                {
                    await _cacheService.setCacheResponceAsync(cacheKey , response.Value ,TimeSpan.FromSeconds(_timeToLiveInSec));
                }
            }
        }
        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            StringBuilder cacheKey = new StringBuilder();
            cacheKey.Append($"{request.Path}");
            foreach (var (key ,value) in request.Query.OrderBy(x => x.Key))
            {
                cacheKey.Append($"|{key} - {value}");
            }
            return cacheKey.ToString();
        }
    }
}
