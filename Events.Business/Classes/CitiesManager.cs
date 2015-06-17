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
            : base(cacheManager)
        {
            Name = "Cities";
            this.dataProvider = dataProvider;
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
    }
}
