using System.Collections.Generic;
using Events.Business.Models;

namespace Events.Business.Interfaces
{
    public interface ISubscribersDataProvider
    {
        int GetCount(string EventId);

        List<Subscriber> GetAllSubscribers(string EventId);

        void SubscribeUser(Subscribing row);
    }
}
