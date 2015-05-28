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
        private string _applicationName;

        INHibernateRoleDataProvider dataProvider;

        public override string ApplicationName
        {
            get { return _applicationName; }
            set { _applicationName = value; }
        }

        private Role GetRole(string rolename)
        {
            Role role = null;
            role = dataProvider.GetRole(rolename);
            return role;
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
                    throw new ProviderException(String.Format("Role name {0} not found.", rolename));
            }

            foreach (string username in usernames)
            {
                if (username.Contains(","))
                    throw new ArgumentException(String.Format("User names {0} cannot contain commas.", username));

                foreach (string rolename in rolenames)
                {
                    if (IsUserInRole(username, rolename))
                        throw new ProviderException(String.Format("User {0} is already in role {1}.", username, rolename));
                }
            }

            dataProvider.AddUsersToRoles(usernames, rolenames);
        }

        public override void CreateRole(string rolename)
        {
            if (rolename.Contains(","))
                throw new ArgumentException("Role names cannot contain commas.");

            if (RoleExists(rolename))
                throw new ProviderException("Role name already exists.");

            dataProvider.CreateRole(rolename);
        }

        public override bool DeleteRole(string rolename, bool throwOnPopulatedRole)
        {
            bool deleted = false;
            if (!RoleExists(rolename))
                throw new ProviderException("Role does not exist.");

            if (throwOnPopulatedRole && GetUsersInRole(rolename).Length > 0)
                throw new ProviderException("Cannot delete a populated role.");

            dataProvider.DeleteRole(rolename);

            return deleted;
        }

        public override string[] GetAllRoles()
        {
            StringBuilder sb = new StringBuilder();

            ICollection<Role> allRole;

            allRole = dataProvider.GetAllRoles();

            foreach (Role r in allRole)
            {
                sb.Append(r.Name + ",");
            }

            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
                return sb.ToString().Split(',');
            }

            return new string[0];
        }

        public override string[] GetRolesForUser(string username)
        {
            ICollection<Role> usrRoles = null;
            StringBuilder sb = new StringBuilder();

            usrRoles = dataProvider.GetRolesForUser(username);

            foreach (Role r in usrRoles)
            {
                sb.Append(r.Name + ",");
            }

            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
                return sb.ToString().Split(',');
            }

            return new string[0];
        }

        public override string[] GetUsersInRole(string rolename)
        {
            StringBuilder sb = new StringBuilder();
            ICollection<User> usrs = null;

            usrs = dataProvider.GetUsersInRole(rolename);

            foreach (User u in usrs)
            {
                sb.Append(u.Name + ",");
            }

            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
                return sb.ToString().Split(',');
            }

            return new string[0];
        }

        public override bool IsUserInRole(string username, string rolename)
        {
            return dataProvider.IsUserInRole(username, rolename);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] rolenames)
        {
            foreach (string rolename in rolenames)
            {
                if (!RoleExists(rolename))
                    throw new ProviderException(String.Format("Role name {0} not found.", rolename));
            }

            foreach (string username in usernames)
            {
                foreach (string rolename in rolenames)
                {
                    if (!IsUserInRole(username, rolename))
                        throw new ProviderException(String.Format("User {0} is not in role {1}.", username, rolename));
                }
            }

            dataProvider.RemoveUsersFromRoles(usernames, rolenames);

        }

        public override bool RoleExists(string rolename)
        {
            return dataProvider.RoleExists(rolename);
        }

        public override string[] FindUsersInRole(string rolename, string usernameToMatch)
        {
            StringBuilder sb = new StringBuilder();
            ICollection<User> User = null;

            User = dataProvider.FindUsersInRole(rolename);

            if (User != null)
            {
                foreach (User u in User)
                {
                    if (String.Compare(u.Name, usernameToMatch, true) == 0)
                        sb.Append(u.Name + ",");
                }
            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
                return sb.ToString().Split(',');
            }
            return new string[0];
        }

    }
}
