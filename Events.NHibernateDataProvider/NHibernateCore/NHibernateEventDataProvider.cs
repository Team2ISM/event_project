using System.Collections.Generic;
using NHibernate;
using NHibernate.Cfg;
using BLL;
using BLL.Interfaces;
using BLL.Models;

namespace DAL.NHibernateCore
{
    public class NHibernateEventDataProvider : IEventDataProvider
    {
        public IList<EventModel> GetList()
        {
            using (ISession session = Helper.OpenSession())
            {
                var criteria = session.CreateCriteria(typeof(EventModel));
                return criteria.List<EventModel>();
            }
        }

        public EventModel GetById(string id)
        {
            EventModel Model;
            using (ISession session = Helper.OpenSession())
            {
                Model = session.Get<EventModel>(id);
            }
            return Model;
        }

        public int Create(EventModel model)
        {
            int EmpNo = 0;

            using (ISession session = Helper.OpenSession())
            {
                //Perform transaction
                using (ITransaction tran = session.BeginTransaction())
                {
                    session.Save(model);
                    tran.Commit();
                }
            }
            return EmpNo;
        }


        public void Update(EventModel model)
        {
            using (ISession session = Helper.OpenSession())
            {
                using (ITransaction tran = session.BeginTransaction())
                {
                    session.Update(model);
                    tran.Commit();
                }
            }
        }

        public void Delete(EventModel model)
        {
            using (ISession session = Helper.OpenSession())
            {
                using (ITransaction tran = session.BeginTransaction())
                {
                    session.Delete(model);
                    tran.Commit();
                }
            }
        }
    }
}
