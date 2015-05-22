using System.Collections.Generic;
using NHibernate;
using Events.Business.Interfaces;
using Events.Business.Models;
using Events.Business.Classes;
using System;
using NHibernate.Criterion;


namespace Events.NHibernateDataProvider.NHibernateCore
{
    public class NHibernateEventDataProvider : IEventDataProvider
    {
        public IList<Event> GetAllEvents()
        {
            using (ISession session = Helper.OpenSession())
            {
                var criteria = session.CreateCriteria<Event>();
                criteria.AddOrder(Order.Desc("DateOfCreation"));
                return criteria.List<Event>();
            }
        }
        public IList<Event> GetList(string location, string nDaysToEvent)
        {
            using (ISession session = Helper.OpenSession())
            {
                if (nDaysToEvent != null && location != null)
                {
                    session.EnableFilter("equalDate")
                    .SetParameter("chosenDate", DateTime.Now.AddDays(Convert.ToDouble(nDaysToEvent)))
                    .SetParameter("nowaday", DateTime.Now);
                    session.EnableFilter("equalLocation").SetParameter("chosenLocation", location);
                }
                else if (nDaysToEvent != null)
                {
                    session.EnableFilter("equalDate")
                    .SetParameter("chosenDate", DateTime.Now.AddDays(Convert.ToDouble(nDaysToEvent)))
                    .SetParameter("nowaday", DateTime.Now);
                }
                else if (location != null)
                {
                    session.EnableFilter("equalLocation").SetParameter("chosenLocation", location);
                }
                else
                {
                    session.EnableFilter("effectiveDate").SetParameter("asOfDate", DateTime.Now);
                }
                var criteria = session.CreateCriteria<Event>();
                criteria.Add(Restrictions.Eq("Active", true));
                //criteria.Add(Restrictions.Eq("Checked", true));
                criteria.AddOrder(Order.Asc("FromDate"));
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
            evnt.Active=!evnt.Active;
            this.Update(evnt);
        }

        public void ToggleButtonStatusChecked(string id)
        {
            Event evnt = GetById(id);
            evnt.Checked = true;
            this.Update(evnt);
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
            model.DateOfCreation = DateTime.Now;
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
