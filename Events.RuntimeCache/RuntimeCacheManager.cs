using System;
using System.Runtime.Caching;
using Events.Business.Interfaces;

namespace Events.RuntimeCache
{
    public class RuntimeCacheManager : ICacheManager 
    {
        ObjectCache cache;

        public RuntimeCacheManager() 
        {
            cache = MemoryCache.Default;
        }

        public TValue FromCache<TValue>(string key, Func<TValue> function)
        {
            CacheItem cacheItem = ToCache<TValue>(key,
                () =>
                {
                    return function();
                }); 
            return (TValue)cacheItem.Value;
        }

        public CacheItem ToCache<TValue>(string key, Func<TValue> function)
        {
            CacheItem cacheItem = cache.GetCacheItem(key);
            if (cacheItem == null)
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTimeOffset.Now.AddHours(1);
                cacheItem = new CacheItem(key, function());
                cache.Set(cacheItem, policy);
            }
            return cacheItem;
        }

        public void RemoveFromCache(string key) 
        {
            CacheItem cacheItem = cache.GetCacheItem(key);
            if (cacheItem != null)
            {
                MemoryCache.Default.Remove(cacheItem.Key);
            }
        }

        public void DisposeCache()
        {
            MemoryCache.Default.Dispose();
   
        }
    }
}
