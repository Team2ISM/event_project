using System.Collections.Generic;
using Events.Business.Models;
using Events.Business;
using NHibernate;
using NHibernate.Criterion;
using Events.NHibernateDataProvider.NHibernateCore;
using Events.Business.Interfaces;
using System;

namespace Events.NHibernateDataProvider.DataProviders
{
    public class NHibernateEmailReminderDataProvider : IEmailReminderDataProvider
    {
        public IList<Event> GetListEventsToRemind()
        {
            using (ISession session = Helper.OpenSession())
            {

                session
                    .EnableFilter("eventsToRemind")
                    .SetParameter("startDate", DateTime.Today.AddHours(DateTime.Now.Hour + 1).AddMinutes(-1))
                    .SetParameter("endDate", DateTime.Today.AddDays(2).AddTicks(-1));
                var criteria = session.CreateCriteria<Event>();
                criteria.Add(Restrictions.Eq("Active", true));
                criteria.AddOrder(Order.Asc("FromDate"));
                return criteria.List<Event>();
            }
        }

        public IList<User> GetUsersToRemind(string eventId)
        {
            var result = new List<User>();
            var userProvider = new NHibernateUserDataProvider();
            using (ISession session = Helper.OpenSession())
            {
                var criteria = session.CreateCriteria(typeof(Subscribing));
                criteria.Add(Restrictions.Eq("EventId", eventId));
                var list = criteria.List<Subscribing>();
                foreach (var item in list)
                {
                    result.Add(userProvider.GetById(item.UserId));
                }
            }
            return result;
        }

        public RemindModel GetIsRemindedModel(string eventId)
        {
            using (ISession session = Helper.OpenSession())
            {
                var criteria = session.CreateCriteria(typeof(RemindModel));
                criteria.Add(Restrictions.Eq("EventId", eventId));
                return criteria.UniqueResult<RemindModel>();
            }
        }

        public void SaveOrUpdateIsRemindedModel(RemindModel model)
        {
            using (ISession session = Helper.OpenSession())
            {
                session.SaveOrUpdate(model);
                session.Flush();
            }
        }

        public void ResetRemindModel(string eventId)
        {
            using (ISession session = Helper.OpenSession())
            {
                using (ITransaction trans = session.BeginTransaction())
                {
                    var criteria = session.CreateCriteria(typeof(RemindModel));
                    criteria.Add(Restrictions.Eq("EventId", eventId));
                    var remindModel = criteria.UniqueResult<RemindModel>();
                    if (remindModel != null)
                    {
                        remindModel.Day = remindModel.Hour = false;
                        session.Update(remindModel);
                    }
                    trans.Commit();
                }
            }
        }
    }
}
