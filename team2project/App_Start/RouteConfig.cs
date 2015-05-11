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
                 name: "Home",
                 url: "",
                 defaults: new { controller = "Home", action = "Index" }
             );

            routes.MapRoute(
                name: "EventList",
                url: "eventsList",
                defaults: new { controller = "Event", action = "Index" }
            );

            routes.MapRoute(
                name: "CreateEvent",
                url: "createEvent",
                defaults: new { controller = "Event", action = "Create" }
            );

            routes.MapRoute(
                name: "EventDetails",
                url: "eventDetails/{id}",
                defaults: new { controller = "Event", action = "Details" },
                constraints: new { id = @"\d+" }
            );
            
            routes.MapRoute(
                "NotFound",
                "{*url}",
             new { controller = "Error", action = "Index" }
            );
            
        }
    }
}