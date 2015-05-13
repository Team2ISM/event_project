using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Cfg;

namespace team2project
{
    public class NHibernateSessionManager
    {
        private readonly ISessionFactory sessionFactory;

        public static ISessionFactory SessionFactory
        {
            get { return Instance.sessionFactory; }
        }

        private ISessionFactory GetSessionFactory()
        {
            return sessionFactory;
        }

        public static NHibernateSessionManager Instance
        {
            get
            {
                return NestedSessionManager.sessionManager;
            }
        }

        public static ISession OpenSession()
        {
            return Instance.GetSessionFactory().OpenSession();
        }

        public static ISession CurrentSession
        {
            get
            {
                return Instance.GetSessionFactory().GetCurrentSession();
            }
        }

        private NHibernateSessionManager()
        {
            Configuration configuration = new Configuration().Configure();
            sessionFactory = configuration.BuildSessionFactory();
        }

        class NestedSessionManager
        {
            internal static readonly NHibernateSessionManager sessionManager =
                new NHibernateSessionManager();
        }
    }
}