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

        CommentManager commentManager;

        ICitiesDataProvider citiesProvider;


        public EventManager(IEventDataProvider dataProvider, ICacheManager cacheManager, ICitiesDataProvider citiesProvider, CommentManager commentManager)
        {
            this.dataProvider = dataProvider;
            this.cacheManager = cacheManager;
            this.citiesProvider = citiesProvider;
            this.commentManager = commentManager;
        }

        public IList<Event> GetAllEvents(bool isForAdmin)
        {
            return cacheManager.FromCache<IList<Event>>("Events::allEvents",
                    () =>
                    {
                        return dataProvider.GetList(0, null, "Admin", isForAdmin);
                    });
        }

        public IList<Event> GetList(string period, string location)
        {
            int? days = getDaysCount(period);
            if (days == null)
            {
                return null;
            }

            int daysToEvent = (int)days;

            return cacheManager.FromCache<IList<Event>>("Filter." + period + " - " + location,
                () =>
                {
                    return dataProvider.GetList(daysToEvent, location, null, false);
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

        public void Update(Event model)
        {
            dataProvider.Update(model);
            cacheManager.RemoveFromCache(model.Id);
            cacheManager.ToCache<Event>(model.Id,
                () =>
                {
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
            commentManager.DeleteByEventId(id);
            dataProvider.Delete(dataProvider.GetById(id));
            cacheManager.RemoveFromCache(id);
            cacheManager.ClearCacheByRegion("Events");
            cacheManager.ClearCacheByRegion("eventsList");
        }

        private int? getDaysCount(string period)
        {
            int? days;
            switch (period)
            {
                case "today":
                    days = 1;
                    break;
                case "threedays":
                    days = 3;
                    break;
                case "week":
                    days = 7;
                    break;
                case "all":
                    days = 0;
                    break;
                default:
                    days = null;
                    break;
            }
            return days;
        }
    }
}
