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

        public IList<EventModel> GetList()
        {
            return CacheManager.FromCache<IList<EventModel>>("eventsList",
                () =>
                {
                    return DataProvider.GetList();
                });
        }

        public void Create(string key, EventModel model)
        {
            DataProvider.Create(model);
            CacheManager.ToCache<EventModel>(key,
                () =>
                {
                    return model;
                });
            CacheManager.RemoveFromCache("eventsList");

        }

        public EventModel GetById(string id)
        {
            return CacheManager.FromCache<EventModel>(id,
                () =>
                {
                    return DataProvider.GetById(id);
                });
        }
    }
}
