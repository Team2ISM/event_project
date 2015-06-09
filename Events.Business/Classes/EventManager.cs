using System.Collections.Generic;
using Events.Business.Interfaces;
using Events.Business.Models;
using System;

namespace Events.Business.Classes
{
    public class EventManager : BaseManager
    {
        IEventDataProvider dataProvider;

        CommentManager commentManager;

        ICitiesDataProvider citiesProvider;

        protected override string Name { get; set; }

        public EventManager(IEventDataProvider dataProvider, ICacheManager cacheManager, ICitiesDataProvider citiesProvider, CommentManager commentManager)
        {
            Name = "Events";
            this.dataProvider = dataProvider;
            this.cacheManager = cacheManager;
            this.citiesProvider = citiesProvider;
            this.commentManager = commentManager;
        }

        public IList<Event> GetAllEvents(bool isForAdmin)
        {
            return FromCache<IList<Event>>("list",
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

            return FromCache<IList<Event>>("list" + period + "-" + location,
                () =>
                {
                    return dataProvider.GetList(daysToEvent, location, null, false);
                });
        }

        public void Create(string key, Event model)
        {
            model.DateOfCreation = DateTime.Now;
            dataProvider.Create(model);
            ToCache<Event>(key,
                () =>
                {
                    return model;
                });
            ClearCacheByRegion();
        }

        public void Update(Event model)
        {
            model.Checked = false;
            dataProvider.Update(model);
            RemoveFromCache(model.Id);
            ToCache<Event>(model.Id,
                () =>
                {
                    return model;
                });
            ClearCacheByRegion();
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
            return FromCache<IList<Event>>("userEvents" + email + "past",
                () =>
                {
                    return dataProvider.GetAuthorPastEvents(email);
                });
        }

        public IList<Event> GetAuthorFutureEvents(string email)
        {
            return FromCache<IList<Event>>("userEvents" + email + "future",
                () =>
                {
                    return dataProvider.GetAuthorFutureEvents(email);
                });
        }

        public bool? Deactivate(string id)
        {
            bool? result;
            if ((result = dataProvider.ToggleStatus(id, false)) == true)
            {
                RemoveFromCache(id);
                ClearCacheByRegion(); ;
                result = true;
            }
            return result;
        }

        public bool? Activate(string id)
        {
            bool? result;
            if ((result = dataProvider.ToggleStatus(id, true)) == true)
            {
                RemoveFromCache(id);
                ClearCacheByRegion();
                result = true;
            }
            return result;
        }

        public void MarkAsSeen(string id)
        {
            dataProvider.MarkAsSeen(id);
            RemoveFromCache(id);
            ClearCacheByRegion();
        }

        public void Delete(string id)
        {
            commentManager.DeleteByEventId(id);
            dataProvider.Delete(dataProvider.GetById(id));
            RemoveFromCache(id);
            ClearCacheByRegion();
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
