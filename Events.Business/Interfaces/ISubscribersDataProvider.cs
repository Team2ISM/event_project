using System.Collections.Generic;
using Events.Business.Models;

namespace Events.Business.Interfaces
{
    public interface ISubscribersDataProvider
    {
        int GetCount(string EventId);

        IList<Subscribing> GetAllSubscribers(string EventId);

        void SubscribeUser(Subscribing row);

        void UnsubscribeUser(Subscribing row);

        bool IsSubscribed(string eventId, string userId);
    }
}
