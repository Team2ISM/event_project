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

        public IList<Event> GetAllEvents(bool isForAdmin)
        {
            return cacheManager.FromCache<IList<Event>>("Events::allEvents",
                    () =>
                    {
                        return dataProvider.GetList(null, null, "Admin", isForAdmin);
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
            return cacheManager.FromCache<IList<Event>>("Filter." + nDaysToEvent + " - " + location,
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

        public void Update(Event model) {
            dataProvider.Update(model);
            cacheManager.RemoveFromCache(model.Id);
            cacheManager.ToCache<Event>(model.Id,
                ( ) => {
                    return model;
                });
            cacheManager.ClearCacheByRegion("Events");
            cacheManager.ClearCacheByRegion("eventsList");
        }

        public Event GetById(string id)
        {
            var evntModel = cacheManager.FromCache<Event>(id,
                 () =>
                 {
                     return dataProvider.GetById(id);
                 });
            return evntModel;
        }

        public IList<Event> GetAuthorPastEvents(string email)
        {
            return cacheManager.FromCache<IList<Event>>("userEvents" + email + "past",
                () =>
                {
                    return dataProvider.GetAuthorPastEvents(email);
                });
        }

        public IList<Event> GetAuthorFutureEvents(string email)
        {
            return cacheManager.FromCache<IList<Event>>("userEvents" + email + "future",
                () =>
                {
                    return dataProvider.GetAuthorFutureEvents(email);
                });
        }

        public void ToggleStatus(string id)
        {
            dataProvider.ToggleStatus(id);
            cacheManager.RemoveFromCache(id);
            cacheManager.ClearCacheByRegion("Events");
            cacheManager.ClearCacheByRegion("eventsList");
        }

        public void MarkAsSeen(string id)
        {
            dataProvider.MarkAsSeen(id);
            cacheManager.RemoveFromCache(id);
            cacheManager.ClearCacheByRegion("Events");
            cacheManager.ClearCacheByRegion("eventsList");
        }
        public void Delete(string id)
        {
            dataProvider.Delete(dataProvider.GetById(id));
            cacheManager.RemoveFromCache(id);
            cacheManager.ClearCacheByRegion("Events");
            cacheManager.ClearCacheByRegion("eventsList");
        }
    }
}
