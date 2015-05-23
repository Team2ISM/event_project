using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events.Business;
using Events.Business.Models;
using Events.Business.Interfaces;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transaction;

namespace Events.NHibernateDataProvider.NHibernateCore
{
    public class NHibernateSubscribersDataProvider : ISubscribersDataProvider
    {
        public NHibernateSubscribersDataProvider( ) {
           /* AutoMapper.Mapper.CreateMap<User, Subscriber>();
            AutoMapper.Mapper.CreateMap<Subscriber, User>();*/
        }
        public int GetCount(string EventId) {
            using (ISession session = Helper.OpenSession()) {
                var criteria = session.CreateCriteria(typeof(Subscribing));
                criteria.Add(Restrictions.Eq("EventId", EventId));
                var list = criteria.List<Subscribing>();
                return list.Count;
            }

        }
        public List<Subscriber> GetAllSubscribers(string EventId) {
            var result = new List<Subscriber>();
            var userProvider = new NHibernateUserDataProvider();
            using (ISession session = Helper.OpenSession()) {
                var criteria = session.CreateCriteria(typeof(Subscribing));
                criteria.Add(Restrictions.Eq("EventId", EventId));
                var list = criteria.List<Subscribing>();
                foreach (var item in list) {
                    result.Add(AutoMapper.Mapper.Map<Subscriber>(userProvider.GetById(item.UserId)));
                }
            }
            return result;
        }

        public void SubscribeUser(Subscribing row) {
            using (ISession session = Helper.OpenSession()) {
                using (ITransaction tran = session.BeginTransaction()) {
                    session.Save(row);
                    tran.Commit();
                }
            }
        }
    }
}
