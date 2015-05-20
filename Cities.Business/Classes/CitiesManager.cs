using System.Collections.Generic;
using Cities.Business.Interfaces;
using Cities.Business.Models;
using Events.Business.Interfaces;

namespace Cities.Business.Classes
{
    public class CitiesManager

    {
        ICitiesDataProvider dataProvider;

        ICacheManager cacheManager;

        public CitiesManager(ICitiesDataProvider dataProvider, ICacheManager cacheManager)
        {
            this.dataProvider = dataProvider;
            this.cacheManager = cacheManager;
        }

        public IList<City> GetList()
        {
            return cacheManager.FromCache<IList<City>>("citiesList",
                () =>
                {
                    return dataProvider.GetAll();
                });
        }
        public void Create(string key, City model)
        {
            dataProvider.Create(model);
            cacheManager.ToCache<City>(key,
                () =>
                {
                    return model;
                });
            cacheManager.RemoveFromCache("commentsList");
        }
    }
}
