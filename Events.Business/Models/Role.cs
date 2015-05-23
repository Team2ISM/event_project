using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events.Business.Models;

namespace Events.Business.Models
{
    public class Role
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<User> Users { get; set; }

        public Role()
        {
            Users = new List<User>();
        }
    }
}
