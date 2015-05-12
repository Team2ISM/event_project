using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
namespace team2project
{
    public class AutomapperConfig
    {
        public static void RegisterMaps()
        {
            AutoMapper.Mapper.CreateMap<DAL.Models.EventModel, Models.EventViewModel>();
            AutoMapper.Mapper.CreateMap<Models.EventViewModel, DAL.Models.EventModel>();
        }
    }
}