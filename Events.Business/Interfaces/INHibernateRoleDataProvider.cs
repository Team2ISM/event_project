using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events.Business.Models;

namespace Events.Business.Interfaces
{
    public interface INHibernateRoleDataProvider
    {
        Role GetRole(string rolename);

        ICollection<Role> GetAllRoles();

        ICollection<Role> GetRolesForUser(string username);

        ICollection<User> GetUsersInRole(string rolename);

        bool IsUserInRole(string username, string rolename);
    }
}
