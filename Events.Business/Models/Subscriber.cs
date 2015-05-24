using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Business.Models
{
    public class Subscriber
    {
        public virtual string Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Surname { get; set; }
    }
}
