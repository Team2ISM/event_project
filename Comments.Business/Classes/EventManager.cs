using System.Collections.Generic;
using Events.Business.Interfaces;
using Events.Business.Models;

namespace Events.Business.Classes
{
    public class EventManager
    {
        IEventDataProvider dataProvider;

        ICacheManager cacheManager; 

        public EventManager(IEventDataProvider dataProvider, ICacheManager cacheManager)
        {
            this.dataProvider = dataProvider;
            this.cacheManager = cacheManager;
        }

        public IList<Event> GetList()
        {
            return cacheManager.FromCache<IList<Event>>("eventsList",
                () =>
                {
                    return dataProvider.GetList();
                });
        }

        public void Create(string key, Event model)
        {
            dataProvider.Create(model);
            cacheManager.ToCache<Event>(key,
                () =>
                {
                    return model;
                });
            cacheManager.RemoveFromCache("eventsList");

        }

        public Event GetById(string id)
        {
            return cacheManager.FromCache<Event>(id,
                () =>
                {
                    return dataProvider.GetById(id);
                });
        }
    }
}
