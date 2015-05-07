using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
namespace team2project
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "EventList",
                url: "eventsList",
                defaults: new { controller = "Event", action = "Index" }
            );
            routes.MapRoute(
                name: "EventDetails",
                url: "eventDetails/{id}",
                defaults: new { controller = "Event", action = "Details" },
                constraints: new { id = @"\d+" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" }
            );
            routes.MapRoute(
                name: "Error",
                url: "{*url}",
                defaults: new { controller = "Error", action = "Index" }
            );
        }
    }
}