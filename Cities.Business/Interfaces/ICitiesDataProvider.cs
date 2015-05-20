using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;
using Cities.Business.Models;


namespace Cities.Business.Interfaces
{
    public interface ICitiesDataProvider
    {
        IList<CitiesModel> GetAll();

        CitiesModel GetById(string commentId);

        int Create(CitiesModel comment);
    }
}
