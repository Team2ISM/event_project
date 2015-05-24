using System.Collections.Generic;
using Events.Business.Interfaces;
using Events.Business.Models;

namespace Events.Business.Classes
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
