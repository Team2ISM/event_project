using System.Collections.Generic;
using Events.Business.Models;
using Events.Business.Helpers;

namespace Events.Business.Interfaces
{
    public interface IEventDataProvider
    {
        IList<Event> GetList(int nDaysToEvent = 0, string location = null);

        IList<Event> GetAllEvents();

        IList<Event> GetRangedEvents(int period = 0, string location = null, int startRow = 0, int rowsCount = 10);

        IList<Event> Find(string text, int period = 0, string location = null);


        IList<Event> GetMyFutureEvents(string userId);

        IList<Event> GetMyPastEvents(string userId);

        Event GetById(string id);

        void Delete(Event model);

        EventStatuses ToggleStatus(string id, bool status);

        void Create(Event evnt);

        void Update(Event model);

        void MarkAsSeen(string id);
    }
}
