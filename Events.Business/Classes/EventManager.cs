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

        RemindManager remindManager;

        protected override string Name { get; set; }

        public EventManager(IEventDataProvider dataProvider, ICacheManager cacheManager, ICitiesDataProvider citiesProvider, CommentManager commentManager, RemindManager remindManager)
            : base(cacheManager)
        {
            Name = "Events";
            this.dataProvider = dataProvider;
            this.citiesProvider = citiesProvider;
            this.commentManager = commentManager;
            this.remindManager = remindManager;
        }

        public IList<Event> GetAllEvents()
        {
            return FromCache<IList<Event>>("AdminList",
                    () =>
                    {
                        return dataProvider.GetAllEvents();
                    });
        }


        public IList<Event> GetMyFutureEvents(string userId)
        {
            return FromCache<IList<Event>>("AdminList",
                    () =>
                    {
                        return dataProvider.GetMyFutureEvents(userId);
                    });
        }

        public IList<Event> GetMyPastEvents(string userId)
        {
            return FromCache<IList<Event>>("AdminList",
                    () =>
                    {
                        return dataProvider.GetMyPastEvents(userId);
                    });
        }


        public IList<Event> GetList(string period, string locationId)
        {
            int? days = getDaysCount(period);

            if (!days.HasValue)
            {
                throw new ArgumentException();
            }

            return FromCache<IList<Event>>("list" + period + "-" + locationId,
                () =>
                {
                    return dataProvider.GetList(days.Value, locationId);
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
            remindManager.ResetRemindModel(model.Id);
            ClearCache();
            ToCache<Event>(model.Id,
                () =>
                {
                    return model;
                });
        }

        public Event GetById(string id)
        {
            return CacheManager.FromCache<Event>(id,
                 () =>
                 {
                     return dataProvider.GetById(id);
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
