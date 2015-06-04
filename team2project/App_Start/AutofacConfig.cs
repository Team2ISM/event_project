using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using Events.RuntimeCache;
using Events.Business.Classes;
using Events.Business.Interfaces;
using Events.NHibernateDataProvider;
using Events.NHibernateDataProvider.NHibernateCore;

namespace team2project
{
    public class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<EventManager>();
            builder.RegisterType<CitiesManager>();
            builder.RegisterType<CommentManager>();
            builder.RegisterType<UserManager>();
            builder.RegisterType<SubscribersManager>();
            builder.RegisterType<NHibernateSubscribersDataProvider>()
                .As<ISubscribersDataProvider>();
            builder.RegisterType<NHibernateEventDataProvider>()
                .As<IEventDataProvider>();
            builder.RegisterType<NHibernateCitiesDataProvider>()
                .As<ICitiesDataProvider>();
            builder.RegisterType<NHibernateCommentDataProvider>()
                .As<ICommentDataProvider>();
            builder.RegisterType<NHibernateSubscribersDataProvider>()
                .As<ISubscribersDataProvider>();
            builder.RegisterType<NHibernateUserDataProvider>()
                .As<IUserDataProvider>();
            builder.RegisterType<RuntimeCacheManager>()
                .As<ICacheManager>();
            builder.RegisterType<NHibernateRoleDataProvider>().AsImplementedInterfaces();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}