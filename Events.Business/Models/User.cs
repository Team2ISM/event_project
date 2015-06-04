using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events.Business.Models;

namespace Events.Business
{
    public class User
    {
        public virtual string Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Surname { get; set; }

        public virtual string Email { get; set; }

        public virtual string Password { get; set; }

        public virtual string PasswordSalt { get; set; }

        public virtual bool IsActive { get; set; }

        public virtual IList<Role> Roles { get; set; }

        public virtual int LocationId { get; set; }

        public virtual void AddRole(Role role)
        {
            role.Users.Add(this);
            Roles.Add(role);
        }

        public virtual void RemoveRole(Role role)
        {
            role.Users.Remove(this);
            Roles.Remove(role);
        }

    }
}
