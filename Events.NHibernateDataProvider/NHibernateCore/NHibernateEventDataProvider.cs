using System.Collections.Generic;
using NHibernate;
using Events.Business.Interfaces;
using Events.Business.Models;
using Events.Business.Classes;


namespace Events.NHibernateDataProvider.NHibernateCore
{
    public class NHibernateEventDataProvider : IEventDataProvider
    {

        public IList<Event> GetList()
        {
            using (ISession session = Helper.OpenSession())
            {
                var criteria = session.CreateCriteria(typeof(Event));
                return criteria.List<Event>();
            }
        }

        public Event GetById(string id)
        {
            Event Model;
            using (ISession session = Helper.OpenSession())
            {
                Model = session.Get<Event>(id);
            }
            return Model;
        }


        public void ToggleButtonStatusActive(string id)
        {
            Event evnt = GetById(id);
            if (evnt.Active == true)
            {
                evnt.Active = false;
                evnt.Checked = true;
            }
            else
                evnt.Active = true;
            using (ISession session = Helper.OpenSession())
            {
                session.Update(evnt);
            }
            
        }

        public void ToggleButtonStatusChecked(string id, bool status)
        {
            Event evnt = GetById(id);
            if (status == false) evnt.Checked = false;
            else
                evnt.Checked = true;
            using (ISession session = Helper.OpenSession())
            {
                session.Update(evnt);
            }
        }

        public int Create(Event model)
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

        public void Update(Event model)
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

        public void Delete(Event model)
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
