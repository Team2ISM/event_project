using System.Collections.Generic;
using NHibernate;
using NHibernate.Cfg;

namespace DAL.NHibernateCore
{
        public class DataAccessLayer<TModel>
        {
            //Define the session factory, this is per database 
            ISessionFactory sessionFactory;

            /// <summary>
            /// Method to create session and manage entities
            /// </summary>
            /// <returns></returns>
            ISession OpenSession()
            {
                if (sessionFactory == null)
                {
                    var cgf = new Configuration();
                    var data = cgf.Configure(System.AppDomain.CurrentDomain.BaseDirectory + "..\\DAL\\NHibernateCore\\Configuration\\hibernate.cfg.xml");
                    cgf.AddDirectory(new System.IO.DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory + "..\\DAL\\NHibernateCore\\Mappings"));
                    sessionFactory = data.BuildSessionFactory();
                }

                return sessionFactory.OpenSession();
            }

            public IList<TModel> GetList()
            {
                using (ISession session = OpenSession())
                {
                    var criteria = session.CreateCriteria(typeof(TModel));
                    return criteria.List<TModel>();
                }
            }

            public TModel GetById(string Id)
            {
                TModel Model;
                using (ISession session = OpenSession())
                {
                    Model = session.Get<TModel>(Id);
                }
                return Model;
            }

            public int Create(TModel model)
            {
                int EmpNo = 0;

                using (ISession session = OpenSession())
                {
                    //Perform transaction
                    using (ITransaction tran = session.BeginTransaction())
                    {
                        session.Save(model);
                        tran.Commit();
                    }
                }
                return EmpNo;
            }


            public void Update(TModel model)
            {
                using (ISession session = OpenSession())
                {
                    using (ITransaction tran = session.BeginTransaction())
                    {
                        session.Update(model);
                        tran.Commit();
                    }
                }
            }

            public void Delete(TModel model)
            {
                using (ISession session = OpenSession())
                {
                    using (ITransaction tran = session.BeginTransaction())
                    {
                        session.Delete(model);
                        tran.Commit();
                    }
                }
            }
        }
}
