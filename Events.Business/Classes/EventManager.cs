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

        ICitiesDataProvider citiesProvider;


        public EventManager(IEventDataProvider dataProvider, ICacheManager cacheManager, ICitiesDataProvider citiesProvider)
        {
            this.dataProvider = dataProvider;
            this.cacheManager = cacheManager;
            this.citiesProvider = citiesProvider;
        }

        public IList<Event> GetAllEvents(bool isForAdmin)
        {
            return cacheManager.FromCache<IList<Event>>("Events::allEvents",
                    () =>
                    {
                        return dataProvider.GetList(0, null, "Admin", isForAdmin);
                    });
        }

        //public IList<Event> GetList()
        //{
        //    return cacheManager.FromCache<IList<Event>>("eventsList",
        //        () =>
        //        {
        //            return dataProvider.GetList();
        //        });
        //}

        public IList<Event> GetList(string period, string location)
        {
            int days;
            var loc = "";
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
                    return new List<Event>();
            }

            if (!String.IsNullOrEmpty(location))
            {
                City city = citiesProvider.GetByValue(location);
                if (city != null)
                {
                    loc = city.Name;
                }
            }

            return cacheManager.FromCache<IList<Event>>("Filter." + period + " - " + loc,
                () =>
                {
                    return dataProvider.GetList(days, loc, null, false);
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

            if (evntModel == null)
            {
                Event evnt = new Event();
                evnt.AuthorId = "undefinded";
                return evnt;
            }
            if (evntModel.Active == false)
            {
                return null;
            }
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

        public void ToggleButtonStatusActive(string id)
        {
            dataProvider.ToggleButtonStatusActive(id);
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
