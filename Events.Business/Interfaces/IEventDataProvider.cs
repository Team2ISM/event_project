using System.Collections.Generic;
using Events.Business.Models;
using Events.Business.Helpers;

namespace Events.Business.Interfaces
{
    public interface IEventDataProvider
    {
        IList<Event> GetList(int nDaysToEvent = 0, string location = null);

        IList<Event> GetAllEvents();

        IList<Event> GetMyFutureEvents(IList<Subscribing> subscribing);

        IList<Event> GetMyPastEvents(IList<Subscribing> subscribing);

        Event GetById(string id);

        void Delete(Event model);

        EventStatuses ToggleStatus(string id, bool status);

        void Create(Event evnt);

        void Update(Event model);

        void MarkAsSeen(string id);
    }
}
