using System.Collections.Generic;
using Events.Business.Models;

namespace Events.Business.Interfaces
{
    public interface IEventDataProvider
    {
        IList<Event> GetList(string nDaysToEvent = null, string location = null, string onlyAvailableData = null);

        Event GetById(string id);

        void Delete(Event model);

        void ToggleButtonStatusChecked(string id);

        void ToggleButtonStatusActive(string id);

        int Create(Event evnt);
    }
}
