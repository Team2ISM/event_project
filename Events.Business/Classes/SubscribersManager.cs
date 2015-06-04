using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Events.Business.Interfaces;
using Events.Business.Models;

namespace Events.Business.Classes
{
    public class SubscribersManager
    {
        private ISubscribersDataProvider dataProvider;

        private ICacheManager cacheManager;

        public SubscribersManager(ISubscribersDataProvider dataProvider, ICacheManager cacheManager)
        {
            this.dataProvider = dataProvider;
            this.cacheManager = cacheManager;
        }

        public IList<Subscriber> GetAllSubscribers(string eventId)
        {
            return cacheManager.FromCache<IList<Subscriber>>("Subsribers." + eventId,
                () =>
                {
                    return dataProvider.GetAllSubscribers(eventId);
                });
        }

        public int GetCount(string eventId)
        {
            return cacheManager.FromCache<int>("Subscribers.Count." + eventId,
                () =>
                {
                    return dataProvider.GetCount(eventId);
                });         
        }

        [Authorize]
        public void SubscribeUser(Subscribing row)
        {
            dataProvider.SubscribeUser(row);
        }

        [Authorize]
        public void UnsubscribeUser(Subscribing row)
        {
            dataProvider.UnsubscribeUser(row);
        }

        public bool IsSubscribed(Subscribing row)
        {
            return cacheManager.FromCache<bool>("Subscribers." + row.EventId + "." + row.UserId,
                () =>
                {
                    return dataProvider.IsSubscribed(row);
                });
        }
    }
}
