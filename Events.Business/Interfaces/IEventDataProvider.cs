using System.Collections.Generic;
using Events.Business.Models;

namespace Events.Business.Interfaces
{
    public interface IEventDataProvider
    {
        IList<Event> GetList();

        Event GetById(string id);

        int Create(Event evnt);

        //void Update(TModel model);

        //void Delete(TModel model);
    }
}
