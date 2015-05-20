using System.Collections.Generic;
using Events.Business.Models;

namespace Events.Business.Interfaces
{
    public interface IEventDataProvider
    {
        IList<Event> GetList();

        Event GetById(string id);

        void Delete(Event model);

        void ToggleButtonStatus(Event evnt);

        int Create(Event evnt);
    }
}
