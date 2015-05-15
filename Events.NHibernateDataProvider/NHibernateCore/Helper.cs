using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NHibernate;
using NHibernate.Cfg;
using BLL.Models;
namespace DAL.NHibernateCore
{
    class Helper
    {
        private static ISessionFactory sessionFactory;

        public static ISession OpenSession()
        {
            if (sessionFactory == null)
            {
                var config = new Configuration();
                var data = config.Configure();
                config.AddAssembly(typeof(Helper).Assembly);
                sessionFactory = data.BuildSessionFactory();
            }

            return sessionFactory.OpenSession();
        }
    }
}
