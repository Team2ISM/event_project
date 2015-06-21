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

        public override string[] GetAllRoles()
        {
            ICollection<Role> allRole;
            allRole = dataProvider.GetAllRoles();
            return allRole.Select(u => u.Name).ToArray();
        }

        public override string[] GetRolesForUser(string username)
        {
            ICollection<Role> usrRoles;
            usrRoles = dataProvider.GetRolesForUser(username);
            return usrRoles.Select(u => u.Name).ToArray();
        }

        public override string[] GetUsersInRole(string rolename)
        {
            ICollection<User> usrs;
            usrs = dataProvider.GetUsersInRole(rolename);
            return usrs.Select(u => u.Name).ToArray();
        }

        public override bool IsUserInRole(string username, string rolename)
        {
            return dataProvider.IsUserInRole(username, rolename);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] rolenames)
        {
        }

        public override string[] FindUsersInRole(string rolename, string usernameToMatch)
        {
            return null;
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }

    public static class ArrayExtention
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
