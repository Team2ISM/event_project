using System.Collections.Generic;
using DAL.NHibernateCore;

namespace BLL.Classes
{
    public class BusinessLogicLayer<TViewModel, TModel>
    {
        DataAccessLayer<TModel> Model;

        public BusinessLogicLayer()
        {
            Model = new DataAccessLayer<TModel>();
        }

        public IList<TViewModel> GetList()
        {
            var ListModels = Model.GetList();
            List<TViewModel> ListViewModels = new List<TViewModel>();
            AutoMapper.Mapper.CreateMap<TModel, TViewModel>();
            foreach (var item in ListModels)
            {
                ListViewModels.Add(AutoMapper.Mapper.Map<TViewModel>(item));
            }
            return ListViewModels;
        }

        public void Create(TViewModel viewModel)
        {
            AutoMapper.Mapper.CreateMap<TViewModel, TModel>();
            TModel model = AutoMapper.Mapper.Map<TModel>(viewModel);
            Model.Create(model);
        }

        public TViewModel GetById(string id)
        { 
            AutoMapper.Mapper.CreateMap<TModel, TViewModel>();
            return AutoMapper.Mapper.Map<TViewModel>(Model.GetById(id));
        }
    }
}
