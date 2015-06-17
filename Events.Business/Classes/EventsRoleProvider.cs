using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using Events.Business.Models;
using Events.Business.Interfaces;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Web.Mvc;

namespace Events.Business.Classes
{
    public class EventsRoleProvider : RoleProvider
    {
        INHibernateRoleDataProvider dataProvider;

        public override string ApplicationName { get; set; }

        private Role GetRole(string rolename)
        {
            return dataProvider.GetRole(rolename);
        }

        public override void Initialize(string name, NameValueCollection config)
        {

            dataProvider = (INHibernateRoleDataProvider)DependencyResolver.Current.GetService(typeof(INHibernateRoleDataProvider));
            base.Initialize(name, config);
        }

        public override void AddUsersToRoles(string[] usernames, string[] rolenames)
        {
            foreach (string rolename in rolenames)
            {
                if (!RoleExists(rolename))
                {
                    throw new ProviderException(String.Format("Role name {0} not found.", rolename));
                }
            }

            foreach (string username in usernames)
            {
                if (username.Contains(","))
                {
                    throw new ArgumentException(String.Format("User names {0} cannot contain commas.", username));
                }
                foreach (string rolename in rolenames)
                {
                    if (IsUserInRole(username, rolename))
                    {
                        throw new ProviderException(String.Format("User {0} is already in role {1}.", username, rolename));
                    }
                }
            }
            dataProvider.AddUsersToRoles(usernames, rolenames);
        }

        public override void CreateRole(string rolename)
        {
            if (rolename.Contains(","))
            {
                throw new ArgumentException("Role names cannot contain commas.");
            }
            if (RoleExists(rolename))
            {
                throw new ProviderException("Role name already exists.");
            }
            dataProvider.CreateRole(rolename);
        }

        public override bool DeleteRole(string rolename, bool throwOnPopulatedRole)
        {
            if (!RoleExists(rolename))
            {
                throw new ProviderException("Role does not exist.");
            }
            if (throwOnPopulatedRole && GetUsersInRole(rolename).Length > 0)
            {
                throw new ProviderException("Cannot delete a populated role.");
            }
            dataProvider.DeleteRole(rolename);
            return false;
        }

        public override string[] GetAllRoles()
        {
            ICollection<Role> allRole;
            allRole = dataProvider.GetAllRoles();
            return allRole.Extract<Role>(e => e.Name);
        }

        public override string[] GetRolesForUser(string username)
        {
            ICollection<Role> usrRoles;
            usrRoles = dataProvider.GetRolesForUser(username);
            return usrRoles.Extract<Role>(e => e.Name);
        }

        public override string[] GetUsersInRole(string rolename)
        {
            ICollection<User> usrs;
            usrs = dataProvider.GetUsersInRole(rolename);
            return usrs.Extract<User>(e => e.Name);
        }

        public override bool IsUserInRole(string username, string rolename)
        {
            return dataProvider.IsUserInRole(username, rolename);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] rolenames)
        {
        }

        public override bool RoleExists(string rolename)
        {
            return dataProvider.RoleExists(rolename);
        }

        public override string[] FindUsersInRole(string rolename, string usernameToMatch)
        {
            return null;
        }
    }

    private static class Helper
    {
        public static string[] Extract<T>(this ICollection<T> container, Func<T, string> lambda)
        {
            var result = new List<string>();
            foreach (var element in container)
            {
                result.Add(lambda(element));
            }
            return result.ToArray();
        }
    }
}
