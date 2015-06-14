using System.Collections.Generic;
using Events.Business.Models;
using Events.Business;

namespace Events.Business.Interfaces
{
    public interface IEmailReminderDataProvider
    {
        IList<Event> GetListEventsToRemind();

        IList<User> GetUsersToRemind(string EventId);

        IsReminded GetIsRemindedModel(string eventId);

        void SaveOrUpdateIsRemindedModel(IsReminded model);
    }
}
