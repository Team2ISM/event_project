using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NHibernate;
using NHibernate.Cfg;
namespace DAL.NHibernateCore
{
    class Helper
    {
        private static ISessionFactory sessionFactory;

        public static ISession OpenSession()
        {
            if (sessionFactory == null)
            {
                var cgf = new Configuration();
                var data = cgf.Configure();
                cgf.AddDirectory(new System.IO.DirectoryInfo(@"C:\Users\milbro\Source\Repos\team2project\Events.NHibernateDataProvider\NHibernateCore\Mappings"));
                sessionFactory = data.BuildSessionFactory();
            }

            return sessionFactory.OpenSession();
        }
    }
}
