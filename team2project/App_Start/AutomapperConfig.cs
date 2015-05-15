using Events.Business.Models;
using team2project.Models;

namespace team2project
{
    public class AutomapperConfig
    {
        public static void RegisterMaps()
        {
            AutoMapper.Mapper.CreateMap<EventModel, EventViewModel>();
            AutoMapper.Mapper.CreateMap<EventViewModel, EventModel>();
        }
    }
}