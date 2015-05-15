using System.Collections.Generic;
using System.Linq;
using Events.Business.Interfaces;
using Events.Business.Models;

namespace Events.EntityFrameworkDataProvider
{
    public class EntityFrameworkEventsDataProvider : IEventDataProvider
    {

        public IList<EventModel> GetList()
        {
            IList<EventModel> list;
            using (EventsDbContext db = new EventsDbContext())
            {
                list = db.Event.ToList<EventModel>();
            }
            return list;
        }

        public EventModel GetById(string id)
        {
            return new EventModel();
        }

        public int Create(EventModel model)
        {
            using (EventsDbContext db = new EventsDbContext())
            {
                db.Event.Add(model);
            }
            return 1;
        }

        //void Update(TModel model);

        //void Delete(TModel model);
    }
}
