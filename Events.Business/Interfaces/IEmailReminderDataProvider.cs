using System.Collections.Generic;
using Events.Business.Models;
using Events.Business;

namespace Events.Business.Interfaces
{
    public interface IEmailReminderDataProvider
    {
        IList<Event> GetListEventsToRemind();

        IList<User> GetUsersToRemind(string EventId);

        RemindModel GetIsRemindedModel(string eventId);

        void SaveOrUpdateIsRemindedModel(RemindModel model);

        void ResetRemindModel(string eventId);
    }
}
