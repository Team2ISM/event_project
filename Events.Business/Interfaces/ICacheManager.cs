using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
namespace BLL.Interfaces
{
    public interface ICacheManager
    {
        TValue FromCache<TValue>(string key, Func<TValue> function);
        CacheItem ToCache<TValue>(string key, Func<TValue> function);
        void RemoveEventsList();
    }
}
