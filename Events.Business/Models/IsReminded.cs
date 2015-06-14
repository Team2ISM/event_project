using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Business.Models
{
    public class RemindModel
    {
        public virtual string Id { get; set; }

        public virtual string EventId { get; set; }

        public virtual bool Day { get; set; }

        public virtual bool Hour { get; set; }

        public RemindModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
