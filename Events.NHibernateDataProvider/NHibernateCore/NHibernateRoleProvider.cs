//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web.Security;
//using Events.Business.Models;
//using NHibernate;
//using NHibernate.Criterion;
//using Events.Business;
//using System.Configuration.Provider;
//using System.Collections.Specialized;

//namespace Events.NHibernateDataProvider.NHibernateCore
//{
//    public class NHibernateRoleProvider : RoleProvider
//    {
//        private string userNameColumn = "Email";

//        private string _applicationName;

//        public override string ApplicationName
//        {
//            get { return _applicationName; }
//            set { _applicationName = value; }
//        }

//        private Role GetRole(string rolename)
//        {
//            Role role = null;
//            using (ISession session = Helper.OpenSession())
//            {
//                using (ITransaction transaction = session.BeginTransaction())
//                {
//                    role = session.CreateCriteria(typeof(Role))
//                        .Add(NHibernate.Criterion.Restrictions.Eq("Name", rolename))
//                        .UniqueResult<Role>();
//                    IList<User> us = role.Users;
//                }
//            }
//            return role;
//        }

//        public override void Initialize(string name, NameValueCollection config)
//        {
//            base.Initialize(name, config);
//        }

//        public override void AddUsersToRoles(string[] usernames, string[] rolenames)
//        {
//            User usr = null;
//            foreach (string rolename in rolenames)
//            {
//                if (!RoleExists(rolename))
//                    throw new ProviderException(String.Format("Role name {0} not found.", rolename));
//            }

//            foreach (string username in usernames)
//            {
//                if (username.Contains(","))
//                    throw new ArgumentException(String.Format("User names {0} cannot contain commas.", username));

//                foreach (string rolename in rolenames)
//                {
//                    if (IsUserInRole(username, rolename))
//                        throw new ProviderException(String.Format("User {0} is already in role {1}.", username, rolename));
//                }
//            }

//            using (ISession session = Helper.OpenSession())
//            {
//                using (ITransaction transaction = session.BeginTransaction())
//                {
//                    foreach (string username in usernames)
//                    {
//                        foreach (string rolename in rolenames)
//                        {
//                            usr = session.CreateCriteria(typeof(User))
//                                        .Add(NHibernate.Criterion.Restrictions.Eq(userNameColumn, username))
//                                        .UniqueResult<User>();

//                            if (usr != null)
//                            {
//                                Role role = session.CreateCriteria(typeof(Role))
//                                        .Add(NHibernate.Criterion.Restrictions.Eq("Name", rolename))
//                                        .UniqueResult<Role>();

//                                //Role ole = GetRole(rolename);
//                                usr.AddRole(role);
//                            }
//                        }
//                        session.SaveOrUpdate(usr);
//                    }
//                    transaction.Commit();
//                }
//            }
//        }

//        public override void CreateRole(string rolename)
//        {
//            if (rolename.Contains(","))
//                throw new ArgumentException("Role names cannot contain commas.");

//            if (RoleExists(rolename))
//                throw new ProviderException("Role name already exists.");

//            using (ISession session = Helper.OpenSession())
//            {
//                using (ITransaction transaction = session.BeginTransaction())
//                {
//                    Role role = new Role();
//                    role.Name = rolename;
//                    session.Save(role);
//                    transaction.Commit();
//                }
//            }
//        }

//        public override bool DeleteRole(string rolename, bool throwOnPopulatedRole)
//        {
//            bool deleted = false;
//            if (!RoleExists(rolename))
//                throw new ProviderException("Role does not exist.");

//            if (throwOnPopulatedRole && GetUsersInRole(rolename).Length > 0)
//                throw new ProviderException("Cannot delete a populated role.");

//            using (ISession session = Helper.OpenSession())
//            {
//                using (ITransaction transaction = session.BeginTransaction())
//                {
//                    Role role = GetRole(rolename);
//                    session.Delete(role);
//                    transaction.Commit();
//                }
//            }

//            return deleted;
//        }

//        public override string[] GetAllRoles()
//        {
//            StringBuilder sb = new StringBuilder();
//            using (ISession session = Helper.OpenSession())
//            {
//                using (ITransaction transaction = session.BeginTransaction())
//                {
//                    IList<Role> allRole = session.CreateCriteria(typeof(Role))
//                                    .List<Role>();

//                    foreach (Role r in allRole)
//                    {
//                        sb.Append(r.Name + ",");
//                    }
//                }
//            }

//            if (sb.Length > 0)
//            {
//                sb.Remove(sb.Length - 1, 1);
//                return sb.ToString().Split(',');
//            }

//            return new string[0];
//        }

//        public override string[] GetRolesForUser(string username)
//        {
//            User usr = null;
//            IList<Role> usrRoles = null;
//            StringBuilder sb = new StringBuilder();
//            using (ISession session = Helper.OpenSession())
//            {
//                using (ITransaction transaction = session.BeginTransaction())
//                {

//                    usr = session.CreateCriteria(typeof(User))
//                                    .Add(NHibernate.Criterion.Restrictions.Eq(userNameColumn, username))
//                                    .UniqueResult<User>();

//                    if (usr != null)
//                    {
//                        usrRoles = usr.Roles;
//                        foreach (Role r in usrRoles)
//                        {
//                            sb.Append(r.Name + ",");
//                        }
//                    }
//                }
//            }

//            if (sb.Length > 0)
//            {
//                sb.Remove(sb.Length - 1, 1);
//                return sb.ToString().Split(',');
//            }

//            return new string[0];
//        }

//        public override string[] GetUsersInRole(string rolename)
//        {
//            StringBuilder sb = new StringBuilder();
//            using (ISession session = Helper.OpenSession())
//            {
//                using (ITransaction transaction = session.BeginTransaction())
//                {
//                    Role role = session.CreateCriteria(typeof(Role))
//                                    .Add(NHibernate.Criterion.Restrictions.Eq("Name", rolename))
//                                    .UniqueResult<Role>();

//                    IList<User> usrs = role.Users;

//                    foreach (User u in usrs)
//                    {
//                        sb.Append(u.Name + ",");
//                    }
//                }
//            }

//            if (sb.Length > 0)
//            {
//                sb.Remove(sb.Length - 1, 1);
//                return sb.ToString().Split(',');
//            }

//            return new string[0];
//        }

//        public override bool IsUserInRole(string username, string rolename)
//        {
//            bool userIsInRole = false;
//            User usr = null;
//            IList<Role> usrRoles = null;
//            StringBuilder sb = new StringBuilder();
//            using (ISession session = Helper.OpenSession())
//            {
//                using (ITransaction transaction = session.BeginTransaction())
//                {
//                    usr = session.CreateCriteria(typeof(User))
//                                    .Add(NHibernate.Criterion.Restrictions.Eq(userNameColumn, username))
//                                    .UniqueResult<User>();

//                    if (usr != null)
//                    {
//                        usrRoles = usr.Roles;
//                        foreach (Role r in usrRoles)
//                        {
//                            if (r.Name.Equals(rolename))
//                            {
//                                userIsInRole = true;
//                                break;
//                            }
//                        }
//                    }

//                }
//            }
//            return userIsInRole;
//        }

//        public override void RemoveUsersFromRoles(string[] usernames, string[] rolenames)
//        {
//            User usr = null;
//            foreach (string rolename in rolenames)
//            {
//                if (!RoleExists(rolename))
//                    throw new ProviderException(String.Format("Role name {0} not found.", rolename));
//            }

//            foreach (string username in usernames)
//            {
//                foreach (string rolename in rolenames)
//                {
//                    if (!IsUserInRole(username, rolename))
//                        throw new ProviderException(String.Format("User {0} is not in role {1}.", username, rolename));
//                }
//            }

//            using (ISession session = Helper.OpenSession())
//            {
//                using (ITransaction transaction = session.BeginTransaction())
//                {

//                    foreach (string username in usernames)
//                    {
//                        usr = session.CreateCriteria(typeof(User))
//                            .Add(NHibernate.Criterion.Restrictions.Eq(userNameColumn, username))
//                            .UniqueResult<User>();

//                        var Roletodelete = new List<Role>();
//                        foreach (string rolename in rolenames)
//                        {
//                            IList<Role> Role = usr.Roles;
//                            foreach (Role r in Role)
//                            {
//                                if (r.Name.Equals(rolename))
//                                    Roletodelete.Add(r);

//                            }
//                        }
//                        foreach (Role rd in Roletodelete)
//                            usr.RemoveRole(rd);


//                        session.SaveOrUpdate(usr);
//                    }
//                    transaction.Commit();

//                }
//            }

//        }

//        public override bool RoleExists(string rolename)
//        {
//            bool exists = false;

//            StringBuilder sb = new StringBuilder();
//            using (ISession session = Helper.OpenSession())
//            {
//                using (ITransaction transaction = session.BeginTransaction())
//                {

//                    Role role = session.CreateCriteria(typeof(Role))
//                                        .Add(NHibernate.Criterion.Restrictions.Eq("Name", rolename))
//                                        .UniqueResult<Role>();
//                    if (role != null)
//                        exists = true;
//                }
//            }
//            return exists;
//        }

//        public override string[] FindUsersInRole(string rolename, string usernameToMatch)
//        {
//            StringBuilder sb = new StringBuilder();
//            using (ISession session = Helper.OpenSession())
//            {
//                using (ITransaction transaction = session.BeginTransaction())
//                {

//                    Role role = session.CreateCriteria(typeof(Role))
//                                    .Add(NHibernate.Criterion.Restrictions.Eq("Name", this.ApplicationName))
//                                    .UniqueResult<Role>();

//                    IList<User> User = role.Users;
//                    if (User != null)
//                    {
//                        foreach (User u in User)
//                        {
//                            if (String.Compare(u.Name, usernameToMatch, true) == 0)
//                                sb.Append(u.Name + ",");
//                        }
//                    }
//                }
//                if (sb.Length > 0)
//                {
//                    sb.Remove(sb.Length - 1, 1);
//                    return sb.ToString().Split(',');
//                }
//                return new string[0];
//            }
//        }

//    }
//}