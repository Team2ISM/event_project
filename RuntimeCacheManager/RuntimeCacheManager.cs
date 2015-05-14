using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using BLL.Interfaces;
namespace RuntimeCache
{
    public class RuntimeCacheManager : ICacheManager
    {
        ObjectCache cache = MemoryCache.Default;

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
                cacheItem = new CacheItem(key, function());
                cache.Set(cacheItem, policy);
            }
            return cacheItem;
        }
    }
}
