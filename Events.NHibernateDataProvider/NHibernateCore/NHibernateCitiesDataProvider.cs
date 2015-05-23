using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Events.Business.Interfaces;
using Events.Business.Models;

namespace Events.NHibernateDataProvider.NHibernateCore
{
    public class NHibernateCitiesDataProvider : ICitiesDataProvider
    {
        public IList<City> GetAll()
        {
            using (ISession session = Helper.OpenSession())
            {
                return session.CreateCriteria< City>().List<City>();
            }
        }

        public City GetById(int id)
        {
            
            using (ISession session = Helper.OpenSession())
            {
               return session.Get<City>(id);
            }
        }

        public int Create(City model)
        {
            int EmpNo = 0;

            using (ISession session = Helper.OpenSession())
            {
                session.Save(model);
            }
            return EmpNo;
        }
    }
}
