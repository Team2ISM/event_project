using System.Collections.Generic;
using Events.Business.Models;

namespace Events.Business.Interfaces
{
    public interface IEventDataProvider
    {
        IList<Event> GetList();

        Event GetById(string id);

        void Delete(Event model);

        void ToggleButtonStatusChecked(string id, bool status);

        void ToggleButtonStatusActive(string id);

        int Create(Event evnt);
    }
}
