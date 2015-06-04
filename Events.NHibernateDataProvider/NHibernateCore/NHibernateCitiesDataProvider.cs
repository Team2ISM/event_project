using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Events.Business.Interfaces;
using Events.Business.Models;

namespace Events.NHibernateDataProvider.NHibernateCore
{
    public class NHibernateCitiesDataProvider : ICitiesDataProvider
    {
        private IList<City> Cities { get; set; }

        public IList<City> GetAll()
        {
            if (Cities != null)
            {
                return Cities;
            }
            using (ISession session = Helper.OpenSession())
            {
                return session.CreateCriteria<City>().List<City>();
            }
        }

        public City GetById(int id)
        {
            if (Cities != null)
            {
                foreach (var city in Cities)
                    if (city.Id == id) return city;
            }

            using (ISession session = Helper.OpenSession())
            {
                return session.Get<City>(id);
            }
        }

        public City GetByName(string name)
        {
            if (Cities != null)
            {
                foreach (var city in Cities)
                    if (city.Name == name) return city;
            }

            using (ISession session = Helper.OpenSession())
            {
                var creteria = session.CreateCriteria<City>();
                creteria.Add(Restrictions.Eq("Name", name));
                return new List<City>(creteria.List<City>()).ToArray()[0];
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
