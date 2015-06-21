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
        public IList<Event> GetList(int daysToEvent, string location)
        {
            if (daysToEvent == null) return null;
            using (ISession session = Helper.OpenSession())
            {
                var endOfDay = DateTime.Today;
                if (!String.IsNullOrEmpty(location))
                {
                    session.EnableFilter("equalLocation").SetParameter("chosenLocation", location);
                }
                if (daysToEvent > 0)
                {
                    session.EnableFilter("equalDate").SetParameter("nowaday", endOfDay).SetParameter("chosenDate", endOfDay.AddDays(daysToEvent - 1));
                }
                else
                {
                    session.EnableFilter("effectiveDate").SetParameter("asOfDate", endOfDay);
                }
                var criteria = session.CreateCriteria<Event>();
                criteria.Add(Restrictions.Eq("Active", true));
                criteria.AddOrder(Order.Asc("FromDate"));
                return criteria.List<Event>();
            }
        }
        public IList<Event> Find(string text, int daysToEvent, string location)
        {
            using (ISession session = Helper.OpenSession())
            {
                var endOfDay = DateTime.Today;
                if (!String.IsNullOrEmpty(location))
                {
                    session.EnableFilter("equalLocation").SetParameter("chosenLocation", location);
                }
                if (daysToEvent > 0)
                {
                    session.EnableFilter("equalDate").SetParameter("nowaday", endOfDay).SetParameter("chosenDate", endOfDay.AddDays(daysToEvent - 1));
                }
                else
                {
                    session.EnableFilter("effectiveDate").SetParameter("asOfDate", endOfDay);
                }
                var criteria = session.CreateCriteria<Event>();
                criteria.Add(Restrictions.Or(
                    Restrictions.Like("Title", text, MatchMode.Anywhere),
                    Restrictions.Like("TextDescription", text, MatchMode.Anywhere)));
                criteria.Add(Restrictions.Eq("Active", true));
                criteria.AddOrder(Order.Asc("FromDate"));
                return criteria.List<Event>();
            }
        }
        public IList<Event> GetAllEvents()
        {
            using (ISession session = Helper.OpenSession())
            {
                return session.CreateCriteria<Event>()
                        .AddOrder(Order.Asc("Checked"))
                        .AddOrder(Order.Desc("DateOfCreation")).List<Event>();
            }
        }

        public IList<Event> GetMyFutureEvents(string userId)
        {
            using (ISession session = Helper.OpenSession())
            {
                var sub = DetachedCriteria.For<Subscribing>();
                sub.Add(Restrictions.Eq("UserId", userId));
                    sub.SetProjection(Projections.Property( "EventId"));

                var eventCriteria = session.CreateCriteria<Event>();
                eventCriteria.Add(Restrictions.And(Restrictions.Ge("ToDate", DateTime.Now),
                    Subqueries.PropertyIn("Id", sub)))
                    .AddOrder(Order.Asc("FromDate"));
                return eventCriteria.List<Event>();
            }
        }

        public IList<Event> GetMyPastEvents(string userId)
        {
            IList<Event> events = new List<Event>();
            using (ISession session = Helper.OpenSession())
            {
                var sub = DetachedCriteria.For<Subscribing>();
                sub.Add(Restrictions.Eq("UserId", userId));
                sub.SetProjection(Projections.Property("EventId"));

                var eventCriteria = session.CreateCriteria<Event>();
                eventCriteria.Add(Restrictions.And(Restrictions.Lt("ToDate", DateTime.Now),
                    Subqueries.PropertyIn("Id", sub)))
                    .AddOrder(Order.Asc("FromDate"));
                return eventCriteria.List<Event>();
            }
        }

        public Event GetById(string id)
        {
            using (ISession session = Helper.OpenSession())
            {
                return session.Get<Event>(id);
            }
        }

        public EventStatuses ToggleStatus(string id, bool status)
        {
            EventStatuses result = EventStatuses.NotExist;
            Event evnt = GetById(id);
            if (evnt != null)
            {
                result = EventStatuses.WasToggled;
                if (evnt.Active != status)
                {
                    evnt.Active = status;
                    this.Update(evnt);
                    result = EventStatuses.ToggleOK;
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

        public void Create(Event model)
        {
            using (ISession session = Helper.OpenSession())
            {
                session.Save(model);
                session.Flush();
            }
        }

        public void Update(Event model)
        {
            using (ISession session = Helper.OpenSession())
            {
                session.Update(model);
                session.Flush();
            }
        }

        public void Delete(Event model)
        {
            using (ISession session = Helper.OpenSession())
            {
                session.Delete(model);
                session.Flush();
            }
        }
    }
}
