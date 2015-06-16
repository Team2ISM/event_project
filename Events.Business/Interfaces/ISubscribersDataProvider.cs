using System.Collections.Generic;
using Events.Business.Models;

namespace Events.Business.Interfaces
{
    public interface ISubscribersDataProvider
    {
        int GetCount(string EventId);

        IList<Subscribing> GetAllSubscribers(string EventId);

        IList<Subscribing> GetMyEventsId(string userId);

        void SubscribeUser(Subscribing row);

        void UnsubscribeUser(Subscribing row);

        bool IsSubscribed(Subscribing row);
    }
}
