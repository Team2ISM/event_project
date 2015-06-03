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

        void ToggleButtonStatusActive(string id);

        int Create(Event evnt);

        void Update(Event model, string admin = "NoAdmin");

        void MarkAsSeen(string id);
    }
}
