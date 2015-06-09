using System.Collections.Generic;
using Events.Business.Models;

namespace Events.Business.Interfaces
{
    public interface IEventDataProvider
    {
        IList<Event> GetList(int nDaysToEvent = 0, string location = null, string onlyAvailableData = null, bool isForAdmin = false);

        Event GetById(string id);

        IList<Event> GetAuthorPastEvents(string email);

        IList<Event> GetAuthorFutureEvents(string email);

        void Delete(Event model);

        bool? ToggleStatus(string id, bool status);

        int Create(Event evnt);

        void Update(Event model);

        void MarkAsSeen(string id);
    }
}
