using System;
using System.Runtime.Caching;

namespace Events.Business.Interfaces
{
    public interface ICacheManager
    {
        TValue FromCache<TValue>(string key, Func<TValue> function);
        CacheItem ToCache<TValue>(string key, Func<TValue> function);
        void RemoveFromCache(string key);
        void ClearCacheByRegion(string region);
        void ClearCacheByName(string name);
    }
}
