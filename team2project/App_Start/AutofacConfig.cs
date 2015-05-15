using Autofac;
using Autofac.Integration.Mvc;
using BLL;
using BLL.Classes;
using BLL.Models;
using DAL.NHibernateCore;
using System.Web.Mvc;
using team2project.Models;
using RuntimeCache;
using BLL.Interfaces;
using Events.EntityFrameworkDataProvider;
namespace team2project
{
    public class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<EventManager>();
            builder.RegisterType<NHibernateEventDataProvider>()
                .As<IEventDataProvider>();
            builder.RegisterType<RuntimeCacheManager>()
                .As<ICacheManager>();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}