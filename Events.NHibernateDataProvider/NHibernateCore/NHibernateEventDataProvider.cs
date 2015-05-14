using System.Collections.Generic;
using NHibernate;
using NHibernate.Cfg;
using BLL;
using BLL.Interfaces;

namespace DAL.NHibernateCore
{
    public class NHibernateEventDataProvider<TModel> : IEventDataProvider<TModel>
        {
            public IList<TModel> GetList()
            {
                using (ISession session = Helper.OpenSession())
                {
                    var criteria = session.CreateCriteria(typeof(TModel));
                    return criteria.List<TModel>();
                }
            }

            public TModel GetById(string id)
            {
                TModel Model;
                using (ISession session = Helper.OpenSession())
                {
                    Model = session.Get<TModel>(id);
                }
                return Model;
            }

            public int Create(TModel model)
            {
                int EmpNo = 0;

                using (ISession session = Helper.OpenSession())
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
                using (ISession session = Helper.OpenSession())
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
                using (ISession session = Helper.OpenSession())
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
