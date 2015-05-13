using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface IBusiness<TViewModel, TModel>
    {
        IList<TViewModel> GetList();

        void Create(TViewModel viewModel);

        TViewModel GetById(string id);
    }
}
