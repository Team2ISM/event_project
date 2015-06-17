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
                return session.CreateCriteria(typeof(User)).List<User>();
            }
        }

        public User GetById(string id)
        {
            using (ISession session = Helper.OpenSession())
            {
                return session.Get<User>(id);
            }
        }

        public User GetByMail(string email)
        {
            using (ISession session = Helper.OpenSession())
            {
                var criteria = session.CreateCriteria(typeof(User));
                criteria.Add(Restrictions.Eq("Email", email));
                return criteria.UniqueResult<User>();
            }
        }

        public string GetFullName(string email)
        {
            using (ISession session = Helper.OpenSession())
            {
                var criteria = session.CreateCriteria(typeof(User));
                criteria.Add(Restrictions.Eq("Email", email));
                User Model = criteria.UniqueResult<User>();
                return Model.Name + " " + Model.Surname;
            }
        }

        public void CreateUser(User user)
        {
            using (ISession session = Helper.OpenSession())
            {
                session.Save(user);
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
