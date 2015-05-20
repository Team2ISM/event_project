using System.Collections.Generic;
using Cities.Business.Models;


namespace Cities.Business.Interfaces
{
    public interface ICitiesDataProvider
    {
        IList<City> GetAll();

        City GetById(int citytId);

        int Create(City city);
    }
}
