using System.Collections.Generic;
using System.Linq;
using Events.Business.Interfaces;
using Events.Business.Models;

namespace Events.EntityFrameworkDataProvider
{
    public class EntityFrameworkEventsDataProvider : IEventDataProvider
    {

        public IList<Event> GetList()
        {
            IList<Event> list;
            using (EventsDbContext db = new EventsDbContext())
            {
                list = db.Event.ToList<Event>();
            }
            return list;
        }

        public Event GetById(string id)
        {
            return new Event();
        }

        public int Create(Event model)
        {
            using (EventsDbContext db = new EventsDbContext())
            {
                db.Event.Add(model);
                db.SaveChanges();
            }
            return 1;
        }

        //void Update(TModel model);

        //void Delete(TModel model);
    }
}
