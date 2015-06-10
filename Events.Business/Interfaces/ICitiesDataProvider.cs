using System.Collections.Generic;
using Events.Business.Models;


namespace Events.Business.Interfaces
{
    public interface ICitiesDataProvider
    {
        IList<City> GetAll();

        City GetById(int citytId);

        City GetByName(string name);
    }
}
