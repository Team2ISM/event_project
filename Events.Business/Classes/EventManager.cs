using System.Collections.Generic;
using Events.Business.Interfaces;
using Events.Business.Models;

namespace Events.Business.Classes
{
    public class EventManager
    {
        IEventDataProvider DataProvider;

        protected ICacheManager CacheManager { get; private set; }

        public EventManager(IEventDataProvider dataProvider, ICacheManager cacheManager)
        {
            DataProvider = dataProvider;
            CacheManager = cacheManager;
        }

        public IList<Event> GetList()
        {
            return CacheManager.FromCache<IList<Event>>("eventsList",
                () =>
                {
                    return DataProvider.GetList();
                });
        }

        public void Create(string key, Event model)
        {
            DataProvider.Create(model);
            CacheManager.ToCache<Event>(key,
                () =>
                {
                    return model;
                });
            CacheManager.RemoveFromCache("eventsList");

        }

        public Event GetById(string id)
        {
            return CacheManager.FromCache<Event>(id,
                () =>
                {
                    return DataProvider.GetById(id);
                });
        }
    }
}
