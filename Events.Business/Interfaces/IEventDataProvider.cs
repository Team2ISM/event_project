using System.Collections.Generic;
using Events.Business.Models;
using Events.Business.Helpers;

namespace Events.Business.Interfaces
{
    public interface IEventDataProvider
    {
        IList<Event> GetList(int nDaysToEvent = 0, string location = null);

        IList<Event> GetAllEvents();

        Event GetById(string id);

        IList<Event> GetAuthorPastEvents(string email);

        IList<Event> GetAuthorFutureEvents(string email);

        void Delete(Event model);

        EventStatuses ToggleStatus(string id, bool status);

        void Create(Event evnt);

        void Update(Event model);

        void MarkAsSeen(string id);
    }
}
