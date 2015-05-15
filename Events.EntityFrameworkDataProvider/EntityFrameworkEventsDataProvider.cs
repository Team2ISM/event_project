using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models;

namespace Events.EntityFrameworkDataProvider
{
    public class EntityFrameworkEventsDataProvider<TModel> : IEventDataProvider<TModel> where TModel : class, new()
    {

        public IList<TModel> GetList()
        {
            IList<TModel> list;
            using (EventsDbContext<TModel> db = new EventsDbContext<TModel>())
            {
                list = (IList<TModel>)db.Event.ToList<TModel>();
            }
            return (IList<TModel>)list;
        }

        public TModel GetById(string id)
        {
            return new TModel();
        }

        public int Create(TModel model)
        {
            using (EventsDbContext<TModel> db = new EventsDbContext<TModel>())
            {
                db.Event.Add(model);
            }
            return 1;
        }

        //void Update(TModel model);

        //void Delete(TModel model);
    }
}
