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
            Role role = null;
            using (ISession session = Helper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    role = session.CreateCriteria(typeof(Role))
                        .Add(NHibernate.Criterion.Restrictions.Eq("Name", rolename))
                        .UniqueResult<Role>();
                    ICollection<User> us = role.Users;
                }
            }
            return role;
        }

        public void AddUsersToRoles(string[] usernames, string[] rolenames)
        {
            User usr = null;

            using (ISession session = Helper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    foreach (string username in usernames)
                    {
                        foreach (string rolename in rolenames)
                        {
                            usr = session.CreateCriteria(typeof(User))
                                        .Add(NHibernate.Criterion.Restrictions.Eq(userNameColumn, username))
                                        .UniqueResult<User>();

                            if (usr != null)
                            {
                                Role role = session.CreateCriteria(typeof(Role))
                                        .Add(NHibernate.Criterion.Restrictions.Eq("Name", rolename))
                                        .UniqueResult<Role>();

                                //Role ole = GetRole(rolename);
                                usr.AddRole(role);
                            }
                        }
                        session.SaveOrUpdate(usr);
                    }
                    transaction.Commit();
                }
            }
        }

        public void CreateRole(string rolename)
        {
            using (ISession session = Helper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    Role role = new Role();
                    role.Name = rolename;
                    session.Save(role);
                    transaction.Commit();
                }
            }
        }

        public void DeleteRole(string rolename)
        {
            using (ISession session = Helper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    Role role = GetRole(rolename);
                    session.Delete(role);
                    transaction.Commit();
                }
            }
        }

        public ICollection<Role> GetAllRoles()
        {
            ICollection<Role> allRole = null;
            using (ISession session = Helper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    allRole = session.CreateCriteria(typeof(Role))
                                    .List<Role>();
                }
            }

            return allRole;
        }

        public ICollection<Role> GetRolesForUser(string username)
        {
            User usr = null;
            ICollection<Role> usrRoles = null;

            using (ISession session = Helper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {

                    usr = session.CreateCriteria(typeof(User))
                                    .Add(NHibernate.Criterion.Restrictions.Eq(userNameColumn, username))
                                    .UniqueResult<User>();

                    if (usr != null)
                    {
                        usrRoles = usr.Roles;
                    }
                }
            }

            return usrRoles;
        }

        public ICollection<User> GetUsersInRole(string rolename)
        {
            ICollection<User> usrs = null;
            using (ISession session = Helper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    Role role = session.CreateCriteria(typeof(Role))
                                    .Add(NHibernate.Criterion.Restrictions.Eq("Name", rolename))
                                    .UniqueResult<Role>();

                    usrs = role.Users;
                }
            }

            return usrs;
        }

        public bool IsUserInRole(string username, string rolename)
        {
            bool userIsInRole = false;
            User usr = null;
            ICollection<Role> usrRoles = null;
            using (ISession session = Helper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    usr = session.CreateCriteria(typeof(User))
                                    .Add(NHibernate.Criterion.Restrictions.Eq(userNameColumn, username))
                                    .UniqueResult<User>();

                    if (usr != null)
                    {
                        usrRoles = usr.Roles;
                        foreach (Role r in usrRoles)
                        {
                            if (r.Name.Equals(rolename))
                            {
                                userIsInRole = true;
                                break;
                            }
                        }
                    }

                }
            }
            return userIsInRole;
        }

        public void RemoveUsersFromRoles(string[] usernames, string[] rolenames)
        {
            User usr = null;

            using (ISession session = Helper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {

                    foreach (string username in usernames)
                    {
                        usr = session.CreateCriteria(typeof(User))
                            .Add(NHibernate.Criterion.Restrictions.Eq(userNameColumn, username))
                            .UniqueResult<User>();

                        var Roletodelete = new List<Role>();
                        foreach (string rolename in rolenames)
                        {
                            ICollection<Role> Role = usr.Roles;
                            foreach (Role r in Role)
                            {
                                if (r.Name.Equals(rolename))
                                    Roletodelete.Add(r);

                            }
                        }
                        foreach (Role rd in Roletodelete)
                            usr.RemoveRole(rd);


                        session.SaveOrUpdate(usr);
                    }
                    transaction.Commit();

                }
            }

        }

        public bool RoleExists(string rolename)
        {
            bool exists = false;

            StringBuilder sb = new StringBuilder();
            using (ISession session = Helper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {

                    Role role = session.CreateCriteria(typeof(Role))
                                        .Add(NHibernate.Criterion.Restrictions.Eq("Name", rolename))
                                        .UniqueResult<Role>();
                    if (role != null)
                        exists = true;
                }
            }
            return exists;
        }

        public ICollection<User> FindUsersInRole(string rolename)
        {
            ICollection<User> User = null;
            using (ISession session = Helper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {

                    Role role = session.CreateCriteria(typeof(Role))
                                    .Add(NHibernate.Criterion.Restrictions.Eq("Name", rolename))
                                    .UniqueResult<Role>();

                    User = role.Users;
                }
                return User;
            }
        }

    }
}