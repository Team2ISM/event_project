using System.Collections.Generic;
using Events.Business.Interfaces;
using Events.Business.Models;
using System;

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

        public IList<Event> GetAllEvents()
        {
            return cacheManager.FromCache<IList<Event>>("allEvents",
                    () =>
                    {
                        return dataProvider.GetList(null,null,"Admin");
                    });
        }
        public IList<Event> GetList()
        {
            return cacheManager.FromCache<IList<Event>>("eventsList",
                () =>
                {
                    return dataProvider.GetList();
                });
        }

        public IList<Event> GetList(string location, string nDaysToEvent)
        {
            return cacheManager.FromCache<IList<Event>>("Filter."+nDaysToEvent + " - " + location,
                () =>
                {
                    return dataProvider.GetList(location, nDaysToEvent);
                });
        }

        public void Create(string key, Event model)
        {
            model.DateOfCreation = DateTime.Now;
            dataProvider.Create(model);
            cacheManager.ToCache<Event>(key,
                () =>
                {
                    return model;
                });
            cacheManager.ClearCacheByRegion("Events");
            cacheManager.ClearCacheByRegion("eventsList");
        }

        public Event GetById(string id)
        {
            return cacheManager.FromCache<Event>(id,
                () =>
                {
                    return dataProvider.GetById(id);
                });
        }

        public void ToggleButtonStatusActive(string id)
        {
            dataProvider.ToggleButtonStatusActive(id);
            cacheManager.ClearCacheByRegion("Events");
            cacheManager.ClearCacheByRegion("eventsList");
        }

        public void ToggleButtonStatusChecked(string id)
        {
            dataProvider.ToggleButtonStatusChecked(id);
            cacheManager.ClearCacheByRegion("Events");
            cacheManager.ClearCacheByRegion("eventsList");
        }
        public void Delete(string id)
        {
            dataProvider.Delete(dataProvider.GetById(id));
            cacheManager.ClearCacheByRegion("Events");
            cacheManager.ClearCacheByRegion("eventsList");
        }
    }
}
