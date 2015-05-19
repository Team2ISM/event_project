using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Users.Business;

namespace Users.NHibernateDataProvider
{
    class UserNHibernateHelper
    {
        private static ISessionFactory sessionFactory;

        public static ISession OpenSession()
        {
            if (sessionFactory == null)
            {
                var config = new Configuration();
                var data = config.Configure();
                config.AddAssembly(typeof(UserNHibernateHelper).Assembly);
                sessionFactory = data.BuildSessionFactory();
            }

            return sessionFactory.OpenSession();
        }
    }
}
