using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Events.Business.Interfaces;
using Events.Business.Models;
using System.Linq;

namespace Events.NHibernateDataProvider.NHibernateCore
{
    public class NHibernateCitiesDataProvider : ICitiesDataProvider
    {
        private IList<City> Cities { get; set; }

        public NHibernateCitiesDataProvider()
        {
            InitializeCities();
        }

        private void InitializeCities()
        {
            using (ISession session = Helper.OpenSession())
            {
                Cities = session.CreateCriteria<City>().List<City>();
            }
        }

        public IList<City> GetAll()
        {
            return Cities;
        }

        public City GetById(int id)
        {
            return Cities.FirstOrDefault(c => c.Id == id);
        }
    }
}
