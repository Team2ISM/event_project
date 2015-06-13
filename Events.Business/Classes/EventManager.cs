using System.Collections.Generic;
using Events.Business.Interfaces;
using Events.Business.Models;
using System;
using Events.Business.Helpers;

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

        public IList<Event> GetAllEvents()
        {
            return FromCache<IList<Event>>("AdminList",
                    () =>
                    {
                        return dataProvider.GetAllEvents();
                    });
        }

        public IList<Event> GetList(string period, string locationId)
        {
            int? days = getDaysCount(period);

            if (days == null)
            {
                return null;
            }

            int daysToEvent = (int)days;

            return FromCache<IList<Event>>("list" + period + "-" + locationId,
                () =>
                {
                    return dataProvider.GetList(daysToEvent, locationId);
                });
        }

        public void Create(Event model)
        {
            model.DateOfCreation = DateTime.Now;
            dataProvider.Create(model);
            ClearCache();
            ToCache<Event>(model.Id,
                () =>
                {
                    return model;
                });
        }

        public void Update(Event model)
        {
            model.Checked = false;
            dataProvider.Update(model);
            ClearCache();
            ToCache<Event>(model.Id,
                () =>
                {
                    return model;
                });
        }

        public Event GetById(string id)
        {
            return cacheManager.FromCache<Event>(id,
                 () =>
                 {
                     return dataProvider.GetById(id);
                 });
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

        public EventStatuses Deactivate(string id)
        {
            EventStatuses result = dataProvider.ToggleStatus(id, false);
            if (result == EventStatuses.ToggleOK)
            {
                ClearCache();
            }
            return result;
        }

        public EventStatuses Activate(string id)
        {
            EventStatuses result = dataProvider.ToggleStatus(id, true);
            if (result == EventStatuses.ToggleOK)
            {
                ClearCache();
            }
            return result;
        }

        public void MarkAsSeen(string id)
        {
            dataProvider.MarkAsSeen(id);
            ClearCache();
        }

        public void Delete(string id)
        {
            commentManager.DeleteByEventId(id);
            dataProvider.Delete(dataProvider.GetById(id));
            ClearCache();
        }

        private int? getDaysCount(string period)
        {
            int? days;
            switch (period)
            {
                case PeriodStates.Today:
                    days = 1;
                    break;
                case PeriodStates.ThreeDays:
                    days = 3;
                    break;
                case PeriodStates.Week:
                    days = 7;
                    break;
                case PeriodStates.Anytime:
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
