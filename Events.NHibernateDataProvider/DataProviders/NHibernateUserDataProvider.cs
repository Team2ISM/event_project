using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events.Business;
using Events.Business.Interfaces;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transaction;

namespace Events.NHibernateDataProvider.NHibernateCore
{
    public class NHibernateUserDataProvider : IUserDataProvider
    {
        public IList<User> GetAllUsers()
        {
            using (ISession session = Helper.OpenSession())
            {
                var criteria = session.CreateCriteria(typeof(User));
                return criteria.List<User>();
            }
        }

        public User GetById(string id)
        {
            User Model;
            using (ISession session = Helper.OpenSession())
            {
                Model = session.Get<User>(id);
            }
            return Model;
        }

        public User GetByMail(string email)
        {
            User Model;
            using (ISession session = Helper.OpenSession())
            {
                var criteria = session.CreateCriteria(typeof(User));
                criteria.Add(Restrictions.Eq("Email", email));
                Model = criteria.UniqueResult<User>();
            }
            return Model;
        }

        public string GetFullName(string email)
        {
            string name;
            using (ISession session = Helper.OpenSession())
            {
                var criteria = session.CreateCriteria(typeof(User));
                criteria.Add(Restrictions.Eq("Email", email));
                User Model = criteria.UniqueResult<User>();
                name = Model.Name + " " + Model.Surname;
            }
            return name;
        }

        public void CreateUser(User user)
        {
            using (ISession session = Helper.OpenSession())
            {
                session.Save(user);
                session.Flush();
            }
        }

        public void DeleteUser(User user)
        {
            using (ISession session = Helper.OpenSession())
            {
                session.Delete(user);
                session.Flush();
            }
        }

        public void UpdateUser(User user)
        {
            using (ISession session = Helper.OpenSession())
            {
                session.Update(user);
                session.Flush();
            }
        }
    }
}
