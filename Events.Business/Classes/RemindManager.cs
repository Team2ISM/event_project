using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events.Business.Models;
using Events.Business.Interfaces;

namespace Events.Business.Classes
{
    public class RemindManager : BaseManager
    {
        IEmailReminderDataProvider dataProvider;

        protected override string Name { get; set; }

        public RemindManager(IEmailReminderDataProvider dataProvider, ICacheManager cacheManager)
        {
            Name = "Reminder";
            this.dataProvider = dataProvider;
            this.cacheManager = cacheManager;
        }

        public IList<Event> GetListEventsToRemind()
        {
            return FromCache<IList<Event>>("list",
                () =>
                {
                    return dataProvider.GetListEventsToRemind();
                });
        }

        public IList<User> GetUsersToRemind(string eventId)
        {
            return FromCache<IList<User>>("UserToRemindByEventId:" + eventId,
                () =>
                {
                    return dataProvider.GetUsersToRemind(eventId);
                });
        }

        public RemindModel GetIsRemindedModel(string eventId)
        {
            return FromCache<RemindModel>("RemindModel:" + eventId,
                () =>
                {
                    return dataProvider.GetIsRemindedModel(eventId);
                });
        }

        public void SaveOrUpdateIsRemindedModel(RemindModel model)
        {
            dataProvider.SaveOrUpdateIsRemindedModel(model);
            ClearCache();
            ToCache<RemindModel>("RemindModel:" + model.EventId,
                () =>
                {
                    return model;
                });
        }
    }
}
