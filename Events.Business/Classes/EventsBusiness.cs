using System.Collections.Generic;
using Business;

namespace BLL.Classes
{
    public class EventsBusiness<TViewModel, TModel>: IBusiness<TViewModel, TModel>
    {
        IEventDataProvider <TModel> DataProvider;

        public EventsBusiness(IEventDataProvider<TModel> dataProvider)
        {
            DataProvider = dataProvider;
        }

        public IList<TViewModel> GetList()
        {
            var listModels = DataProvider.GetList();
            return AutoMapper.Mapper.Map <List<TViewModel>>(listModels);    
        }

        public void Create(TViewModel viewModel)
        {
            TModel model = AutoMapper.Mapper.Map<TModel>(viewModel);
            DataProvider.Create(model);
        }

        public TViewModel GetById(string id)
        {
            return AutoMapper.Mapper.Map<TViewModel>(DataProvider.GetById(id));
        }
    }
}
