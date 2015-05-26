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
                url: "Events",
                defaults: new { controller = "Event", action = "Index" }
            );

            routes.MapRoute(
              name: "by-Location-Days",
              url: "Events/{loc}/{days}",
              defaults: new { controller = "Event", action = "Filters", days = UrlParameter.Optional },
              constraints: new { days = @"\d+" }
          );

            routes.MapRoute(
                 name: "by-Days",
                 url: "Events/{days}",
                 defaults: new { controller = "Event", action = "Filters", days = UrlParameter.Optional },
                 constraints: new { days = @"\d+" }
             );

            routes.MapRoute(
                 name: "by-Location",
                 url: "Events/{loc}",
                 defaults: new { controller = "Event", action = "Filters", loc = UrlParameter.Optional }
             );

            routes.MapRoute(
                name: "Activate",
                url: "User/Activate/{id}",
                defaults: new { controller = "User", action = "Activate" }
            );

            routes.MapRoute(
                name: "Thankyoupage",
                url: "User/ThankYouPage",
                defaults: new { controller = "User", action = "ThankYouPage" }
            );

            routes.MapRoute(
                name: "Welcome",
                url: "User/Welcome",
                defaults: new { controller = "User", action = "Welcome" }
            );

            routes.MapRoute(
                name: "Registration",
                url: "User/Registration",
                defaults: new { controller = "User", action = "Registration" }
            );

            routes.MapRoute(
                name: "Login",
                url: "User/Login",
                defaults: new { controller = "User", action = "Login" }
            );

            routes.MapRoute(
                name: "ForgotPassword",
                url: "User/ForgotPassword",
                defaults: new { controller = "User", action = "ForgotPassword" }
            );

            routes.MapRoute(
                name: "Logout",
                url: "User/Logout",
                defaults: new { controller = "User", action = "Logout" }
            );

            routes.MapRoute(
                name: "Update",
                url: "User/Update",
                defaults: new { controller = "User", action = "Update" }
            );

            routes.MapRoute(
                name: "CreateEvent",
                url: "createEvent",
                defaults: new { controller = "Event", action = "Create" }
            );
            //новий роут
            routes.MapRoute(
                name: "UpdateEvent",
                url: "eventUpdate/{id}",
               defaults: new { controller = "Event", action = "Update" }
            );
            routes.MapRoute(
               name: "CreateComment",
               url: "CreateComment",
               defaults: new { controller = "Comment", action = "AddComment" }
           );

            routes.MapRoute(
                name: "EventDetails",
                url: "Details/{id}",
                defaults: new { controller = "Event", action = "Details" }
            );

            routes.MapRoute(
                name: "ManagerPage",
                url: "admin",
                defaults: new { controller = "Admin", action = "ManagerPage" }
            );
            routes.MapRoute(
                name: "SetEventChecked",
                url: "SetEventChecked/{id}",
                defaults: new { controller = "Admin", action = "ToggleButtonStatusChecked" }
            );
            routes.MapRoute(
                name: "ToogleIsActiveEvent",
                url: "ToogleIsActiveEvent/{id}",
                defaults: new { controller = "Admin", action = "ToggleButtonStatusActive" }
            );
            routes.MapRoute(
                name: "DeleteEvent",
                url: "DeleteEvent/{id}",
                defaults: new { controller = "Admin", action = "DeleteEvent" }
            );
            routes.MapRoute(
                name: "Subscribers",
                url: "subscribers/{id}",
                defaults: new { controller = "Subscribers", action = "GetSubscribers" }
            );
            routes.MapRoute(
                name: "CountPost",
                url: "getCount",
                defaults: new { controller = "Subscribers", action = "GetCount" }
            );
            routes.MapRoute(
                name: "CountGet",
                url: "getCount/{id}",
                defaults: new { controller = "Subscribers", action = "Index" }
            );
            routes.MapRoute(
                name: "Subscribe",
                url: "Subscribe",
                defaults: new { controller = "Subscribers", action = "Subscribe" }
            );
            routes.MapRoute(
                name: "Unsubscribe",
                url: "Unsubscribe",
                defaults: new { controller = "Subscribers", action = "Unsubscribe" }
            );
            routes.MapRoute(
                name: "IsSubscribed",
                url: "IsSubscribed",
                defaults: new { controller = "Subscribers", action = "IsSubscribed" }
            );
            routes.MapRoute(
                "NotFound",
                "{*url}",
             new { controller = "Error", action = "Index" }
            );
        }
    }
}