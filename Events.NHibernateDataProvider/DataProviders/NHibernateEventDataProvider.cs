using System.Collections.Generic;
using NHibernate;
using Events.Business.Interfaces;
using Events.Business.Models;
using Events.Business.Classes;
using System;
using NHibernate.Criterion;
using Events.Business.Helpers;


namespace Events.NHibernateDataProvider.NHibernateCore
{

    public class NHibernateEventDataProvider : IEventDataProvider
    {

        public IList<Event> GetList(int daysToEvent, string location, string onlyAvailableData, bool isForAdmin = false)
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

                if (!String.IsNullOrEmpty(location))
                {
                    session.EnableFilter("equalLocation").SetParameter("chosenLocation", location);
                }

                if (daysToEvent > 0)
                {
                    session.EnableFilter("equalDate").SetParameter("nowaday", now).SetParameter("chosenDate", now.AddDays(daysToEvent - 1));
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
                    Restrictions.Lt("ToDate", DateTime.Now)));
                return criteria.List<Event>();
            }
        }

        public IList<Event> GetAuthorFutureEvents(string email)
        {
            using (ISession session = Helper.OpenSession())
            {
                var criteria = session.CreateCriteria(typeof(Event));
                criteria.Add(Restrictions.And(Restrictions.Eq("AuthorId", email),
                    Restrictions.Ge("ToDate", DateTime.Now)));
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


        public EventStatus.EventStatuses ToggleStatus(string id, bool status)
        {
            EventStatus.EventStatuses result = EventStatus.EventStatuses.NotExist;
            Event evnt = GetById(id);
            if (evnt != null)
            {
                result = EventStatus.EventStatuses.WasToggled;
                if (evnt.Active != status)
                {
                    evnt.Active = status;
                    this.Update(evnt);
                    result = EventStatus.EventStatuses.ToggleOK;
                }
            }
            return result;
        }

        public void MarkAsSeen(string id)
        {
            Event evnt = GetById(id);
            if (evnt != null)
            {
                evnt.Checked = true;
                this.Update(evnt);
            }
        }

        public int Create(Event model)
        {
            using (ISession session = Helper.OpenSession())
            {
                using (ITransaction tran = session.BeginTransaction())
                {
                    session.Save(model);
                    tran.Commit();
                }
            }
            return 0;
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
