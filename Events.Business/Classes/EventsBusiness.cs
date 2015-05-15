using System.Collections.Generic;
using BLL.Interfaces;

namespace BLL.Classes
{
    public class EventsBusiness<TViewModel, TModel> 
    {
        IEventDataProvider<TModel> DataProvider;

        ICacheManager CacheManager;

        public EventsBusiness(IEventDataProvider<TModel> dataProvider, ICacheManager cacheManager)
        {
            DataProvider = dataProvider;
            CacheManager = cacheManager;
        }

        public IList<TViewModel> GetList()
        {
            var list = CacheManager.FromCache<IList<TModel>>("eventsList", 
                () => { 
                    return DataProvider.GetList(); 
                });
            return AutoMapper.Mapper.Map<List<TViewModel>>(list);
        }

        public void Create(string key, TViewModel viewModel)
        {
            TModel model = AutoMapper.Mapper.Map<TModel>(viewModel);
            CacheManager.ToCache<TModel>(key,
                () =>
                {
                    return model;
                });
            CacheManager.RemoveEventsList();
            DataProvider.Create(model);
        }

        public TViewModel GetById(string id)
        {
            return CacheManager.FromCache<TViewModel>(id,
                () =>
                {
                    return AutoMapper.Mapper.Map<TViewModel>(DataProvider.GetById(id));
                });
        }
    }
}
