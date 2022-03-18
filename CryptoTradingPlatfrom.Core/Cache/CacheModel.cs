﻿using CryptoTradingPlatform.Core.Models.Articles;
using Microsoft.Extensions.Caching.Memory;

namespace CryptoTradingPlatfrom.Core.Cache
{
    public static class CacheModel
    {
        private static IMemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions());

        public static void Add(string cacheKey, List<NewsViewModel> model)
        {
            var cacheExpiryOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromSeconds(20),
            };
            memoryCache.Set(cacheKey, model, cacheExpiryOptions);
        }

        public static List<NewsViewModel> Get(string cacheKey)
        {
            var result = memoryCache.Get(cacheKey);
            return (List<NewsViewModel>)result;
        }

        public static void Delete(string cacheKey)
        {
            memoryCache.Remove(cacheKey);
        }
    }
}