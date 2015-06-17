using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using Events.Business.Models;
using NHibernate;
using NHibernate.Criterion;
using Events.Business;
using System.Configuration.Provider;
using System.Collections.Specialized;
using Events.Business.Interfaces;



namespace Events.NHibernateDataProvider.NHibernateCore
{
    public class NHibernateRoleDataProvider : INHibernateRoleDataProvider
    {
        private string userNameColumn = "Email";

        public Role GetRole(string rolename)
        {
            using (ISession session = Helper.OpenSession())
            {
                return session.CreateCriteria(typeof(Role))
                    .Add(NHibernate.Criterion.Restrictions.Eq("Name", rolename))
                    .UniqueResult<Role>();
            }
        }

        public void AddUsersToRoles(string[] usernames, string[] rolenames)
        {
        }

        public void CreateRole(string rolename)
        {
        }

        public void DeleteRole(string rolename)
        {
        }

        public ICollection<Role> GetAllRoles()
        {
            using (ISession session = Helper.OpenSession())
            {
                return session.CreateCriteria(typeof(Role))
                                .List<Role>();
            }
        }

        public ICollection<Role> GetRolesForUser(string username)
        {
            using (ISession session = Helper.OpenSession())
            {
                User usr = session.CreateCriteria(typeof(User))
                                 .Add(NHibernate.Criterion.Restrictions.Eq(userNameColumn, username))
                                 .UniqueResult<User>();
                return usr.Roles;
            }
        }

        public ICollection<User> GetUsersInRole(string rolename)
        {
            using (ISession session = Helper.OpenSession())
            {
                Role role = session.CreateCriteria(typeof(Role))
                                .Add(NHibernate.Criterion.Restrictions.Eq("Name", rolename))
                                .UniqueResult<Role>();
                return role.Users;
            }
        }

        public bool IsUserInRole(string username, string rolename)
        {
            using (ISession session = Helper.OpenSession())
            {
                var usr = session.CreateCriteria(typeof(User))
                                .Add(NHibernate.Criterion.Restrictions.Eq(userNameColumn, username))
                                .UniqueResult<User>();

                foreach (Role r in usr.Roles)
                {
                    if (r.Name.Equals(rolename))
                    {
                        return true;
                    }
                }

                return false;
            }
        }


        public bool RoleExists(string rolename)
        {
            return true;
        }

    }
}