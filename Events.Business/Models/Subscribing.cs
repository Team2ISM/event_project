using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Business.Models
{
    public class Subscribing
    {
        public Subscribing( ) { }
        public Subscribing(string EventId, string UserId) {
            this.EventId = EventId;
            this.UserId = UserId;
        }
        public virtual string EventId { get; set; }

        public virtual string UserId { get; set; }
    }
}
