using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface IEventDataProvider
    {
        IList<EventModel> GetList();

        EventModel GetById(string id);

        int Create(EventModel evnt);

        //void Update(TModel model);

        //void Delete(TModel model);
    }
}
