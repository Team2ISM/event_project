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
        public IList<Event> GetList(string location, string nDaysToEvent, string onlyAvailableData, bool isForAdmin = false)
        {
            using (ISession session = Helper.OpenSession())
            {
                var now = DateTime.Now.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                if (onlyAvailableData != null)
                {
                    var result = session.CreateCriteria<Event>();
                    if (isForAdmin) result.AddOrder(Order.Asc("Checked"));
                    return result
                        .AddOrder(Order.Desc("DateOfCreation"))                        
                        .List<Event>();
                }
                if (nDaysToEvent != null && location != null)
                {
                    session.EnableFilter("equalDate")
                    .SetParameter("chosenDate", now.AddDays(Convert.ToDouble(nDaysToEvent)))
                    .SetParameter("nowaday", now);
                    session.EnableFilter("equalLocation").SetParameter("chosenLocation", location);
                }
                else if (nDaysToEvent != null)
                {
                    session.EnableFilter("equalDate")
                    .SetParameter("chosenDate", now.AddDays(Convert.ToDouble(nDaysToEvent)))
                    .SetParameter("nowaday", now);
                }
                else if (location != null)
                {
                    session.EnableFilter("equalLocation").SetParameter("chosenLocation", location);
                }
                else
                {
                    session.EnableFilter("effectiveDate").SetParameter("asOfDate", now);
                }
                var criteria = session.CreateCriteria<Event>();
                criteria.Add(Restrictions.Eq("Active", true));
                criteria.AddOrder(Order.Asc("FromDate"));
                return criteria.List<Event>();
            }
        }

        public IList<Event> GetByAuthorMail(string email)
        {
            using (ISession session = Helper.OpenSession())
            {
                var criteria = session.CreateCriteria(typeof(Event));
                criteria.Add(Restrictions.Eq("AuthorId", email));
                return criteria.List<Event>();
            }
        }

        public IList<Event> GetAuthorPastEvents(string email)
        {
            using (ISession session = Helper.OpenSession())
            {
                var criteria = session.CreateCriteria(typeof(Event));
                criteria.Add(Restrictions.And(Restrictions.Eq("AuthorId", email), 
                    Restrictions.Lt("FromDate", DateTime.Now)));
                return criteria.List<Event>();
            }
        }

        public IList<Event> GetAuthorFutureEvents(string email)
        {
            using (ISession session = Helper.OpenSession())
            {
                var criteria = session.CreateCriteria(typeof(Event));
                criteria.Add(Restrictions.And(Restrictions.Eq("AuthorId", email),
                    Restrictions.Gt("FromDate", DateTime.Now)));
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
            this.Update(evnt, "Admin");
        }

        public void MarkAsSeen(string id)
        {
            Event evnt = GetById(id);
            evnt.Checked = true;
            this.Update(evnt, "Admin");
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

        public void Update(Event model, string admin = "NoAdmin")
        {
            if(admin == "NoAdmin") model.DateOfCreation = DateTime.Now;
            model.Checked = true;
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
