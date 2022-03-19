using CryptoTradingPlatform.Core.Models.Api;
using Microsoft.Extensions.Caching.Memory;

namespace CryptoTradingPlatfrom.Core.Cache
{
    public static class CacheModel
    {
        private static IMemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions());

        public static void Add(string cacheKey, List<CryptoResponseModel> model)
        {
            var cacheExpiryOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(40),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromSeconds(20),
            };
            memoryCache.Set(cacheKey, model, cacheExpiryOptions);
        }

        public static List<CryptoResponseModel> Get(string cacheKey)
        {
            var result = memoryCache.Get(cacheKey);
            return (List<CryptoResponseModel>)result;
        }

        public static void Delete(string cacheKey)
        {
            memoryCache.Remove(cacheKey);
        }
    }
}
