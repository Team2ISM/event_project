using System.Collections.Generic;
using Events.Business.Interfaces;
using Events.Business.Models;

namespace Events.Business.Classes
{
    public class CitiesManager : BaseManager

    {
        ICitiesDataProvider dataProvider;

        protected override string Name { get; set; }

        public CitiesManager(ICitiesDataProvider dataProvider, ICacheManager cacheManager)
        {
            Name = "Cities";
            this.dataProvider = dataProvider;
            this.cacheManager = cacheManager;
        }

        public IList<City> GetList()
        {
            return FromCache<IList<City>>("list",
                () =>
                {
                    return dataProvider.GetAll();
                });
        }

        public City GetById(int citytId)
        {
            return FromCache<City>(citytId.ToString(),
                () =>
                {
                    return dataProvider.GetById(citytId);
                });
        }

        public City GetByName(string name)
        {
            return cacheManager.FromCache<City>(name,
                () =>
                {
                    return dataProvider.GetByName(name);
                });
        }

        public void Create(City model)
        {
            dataProvider.Create(model);
            ToCache<City>(model.Id.ToString(),
                () =>
                {
                    return model;
                });
        }
    }
}
