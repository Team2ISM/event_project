using Autofac;
using Autofac.Integration.Mvc;
using Events.Business.Classes;
using Events.NHibernateDataProvider.NHibernateCore;
using Events.EntityFrameworkDataProvider;
using System.Web.Mvc;
using RuntimeCache;
using Events.Business.Interfaces;
using Events.Business;

namespace team2project
{
    public class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<EventManager>();
            builder.RegisterType<EntityFrameworkEventsDataProvider>()
                .As<IEventDataProvider>();
            builder.RegisterType<RuntimeCacheManager>()
                .As<ICacheManager>();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}