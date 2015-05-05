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
            /*routes.MapRoute(
                name: "HomeAct",
                url: "Home/{action}",
                defaults: new { controller = "Home", action = "Index"},
                constraints: new { action = "(index|list)" }
            );
            routes.MapRoute(
                name: "HomeDetails",
                url: "Home/Details/{id}",
                defaults: new { controller = "Home", action = "details"},
                constraints: new { id = @"\d+" }
            );*/
            routes.MapRoute(
                name: "Act",
                url: "{action}",
                defaults: new { controller = "Home", action = "Index"},
                constraints: new { action = "(index|list)" }
            );
            routes.MapRoute(
                name: "Details",
                url: "Details/{id}",
                defaults: new { controller = "Home", action = "details"},
                constraints: new { id = @"\d+" }
            );
        }
    }
}