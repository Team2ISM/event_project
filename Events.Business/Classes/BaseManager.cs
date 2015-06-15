using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events.Business.Interfaces;
using System.Runtime.Caching;
using Events.Business.Helpers;

namespace Events.Business.Classes
{
    public abstract class BaseManager
    {
        protected ICacheManager cacheManager;

        protected abstract string Name { get; set; }

        protected TValue FromCache<TValue>(string name, Func<TValue> function)
        {
            return cacheManager.FromCache<TValue>(Name + "::" + name, function);
        }

        protected CacheItem ToCache<TValue>(string name, Func<TValue> function)
        {
            return cacheManager.ToCache<TValue>(Name + "::" + name, function);
        }

        protected void ClearCache()
        {
            cacheManager.ClearCacheByName(Name);
            if (!Name.Equals(EnvironmentInfo.ReminderCacheName))
            {
                cacheManager.ClearCacheByName(EnvironmentInfo.ReminderCacheName);
            }
        }

        protected void RemoveFromCache(string name)
        {
            cacheManager.RemoveFromCache(Name + "::" + name);
        }
    }
}
