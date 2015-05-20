using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Cities.Business.Interfaces;
using Cities.Business.Models;

namespace Cities.NHibernateDataProvider
{
    public class NHibernateCitiesDataProvider : ICitiesDataProvider
    {
        public IList<City> GetAll()
        {
            using (ISession session = Helper.OpenSession())
            {
                var criteria = session.CreateCriteria<City>();
                return criteria.List<City>();
            }
        }

        public City GetById(int id)
        {
            City Model;
            using (ISession session = Helper.OpenSession())
            {
                Model = session.Get<City>(id);
            }
            return Model;
        }

        public int Create(City model)
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

       

    }
}
