using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IEventDataProvider<TModel>
    {
        IList<TModel> GetList();

        TModel GetById(string id);

        int Create(TModel model);

        //void Update(TModel model);

        //void Delete(TModel model);
    }
}
