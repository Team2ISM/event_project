using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;
namespace team2project
{
    public class AutomapperConfig
    {
        public static void RegisterMaps()
        {
            AutoMapper.Mapper.CreateMap<BLL.Models.EventModel, Models.EventViewModel>();
            AutoMapper.Mapper.CreateMap<Models.EventViewModel, BLL.Models.EventModel>();
        }
    }
}