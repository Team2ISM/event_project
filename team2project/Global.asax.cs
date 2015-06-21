using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using Events.Business.Helpers;

namespace team2project
{

    public class MvcApplication : System.Web.HttpApplication
    {
        public const string MultipleParameterKey = "_multiple_";

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            AutofacConfig.RegisterDependencies();
            AutomapperConfig.RegisterMaps();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest(Object source, EventArgs e)
        {
            MyCultureConfig.SetCulture("ru");
        }    
    }
}