using Events.Business.Models;
using team2project.Models;
using Users.Business;

namespace team2project
{
    public class AutomapperConfig
    {
        public static void RegisterMaps()
        {
            AutoMapper.Mapper.CreateMap<Event, EventViewModel>();
            AutoMapper.Mapper.CreateMap<EventViewModel, Event>();
            AutoMapper.Mapper.CreateMap<UserViewModel, User>();
            AutoMapper.Mapper.CreateMap<User, UserViewModel>();
        }
    }
}