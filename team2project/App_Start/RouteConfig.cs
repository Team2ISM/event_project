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

            #region Events map routes
            routes.MapRoute(
                name: "EventList",
                url: "events",
                defaults: new { controller = "Event", action = "Index" }
            );

            routes.MapRoute(
               name: "CreateEvent",
               url: "events/create",
               defaults: new { controller = "Event", action = "Create" }
           );

            routes.MapRoute(
                name: "UpdateEvent",
                url: "events/update/{id}",
               defaults: new { controller = "Event", action = "Update", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "EventDetails",
               url: "events/details/{id}",
               defaults: new { controller = "Event", action = "Details" }
           );

            routes.MapRoute(
                name: "DeleteUserEvent",
                url: "events/delete/{id}",
                defaults: new { controller = "Event", action = "DeleteEvent" }
            );

            routes.MapRoute(
                 name: "by-Days",
                 url: "events/{days}",
                 defaults: new { controller = "Event", action = "Filters", days = UrlParameter.Optional },
                 constraints: new { days = @"\d+" }
             );

            routes.MapRoute(
                 name: "by-Location",
                 url: "events/{loc}",
                 defaults: new { controller = "Event", action = "Filters", loc = UrlParameter.Optional }
             );

            routes.MapRoute(
              name: "by-Location-Days",
              url: "events/{loc}/{days}",
              defaults: new { controller = "Event", action = "Filters", days = UrlParameter.Optional },
              constraints: new { days = @"\d+" }
          );

            #endregion

            #region User map routes

            routes.MapRoute(
                name: "PastEvents",
                url: "user/myevents/past",
                defaults: new { controller = "User", action = "MyPastEvents" }
            );

            routes.MapRoute(
                name: "FutureEvents",
                url: "user/myevents/future",
                defaults: new { controller = "User", action = "MyFutureEvents" }
            );

            routes.MapRoute(
                name: "MyEvents",
                url: "user/myevents",
                defaults: new { controller = "User", action = "MyEvents"}
            );

            routes.MapRoute(
                name: "Activate",
                url: "user/activate/{id}",
                defaults: new { controller = "User", action = "Activate" }
            );

            routes.MapRoute(
                name: "Thankyoupage",
                url: "user/thankyoupage",
                defaults: new { controller = "User", action = "ThankYouPage" }
            );

            routes.MapRoute(
                name: "Welcome",
                url: "user/welcome",
                defaults: new { controller = "User", action = "Welcome" }
            );

            routes.MapRoute(
                name: "Registration",
                url: "user/registration",
                defaults: new { controller = "User", action = "Registration" }
            );

            routes.MapRoute(
                name: "Login",
                url: "user/login",
                defaults: new { controller = "User", action = "Login" }
            );

            routes.MapRoute(
                name: "ForgotPassword",
                url: "user/forgotpassword",
                defaults: new { controller = "User", action = "ForgotPassword" }
            );

            routes.MapRoute(
                name: "Logout",
                url: "user/logout",
                defaults: new { controller = "User", action = "Logout" }
            );

            routes.MapRoute(
                name: "Update",
                url: "user/update",
                defaults: new { controller = "User", action = "Update" }
            );
            #endregion

            #region Comments map routes

            routes.MapRoute(
               name: "CreateComment",
               url: "comments/create",
               defaults: new { controller = "Comment", action = "AddComment" }
           );
            #endregion

            #region Admin map routes

            routes.MapRoute(
                name: "ManagerPage",
                url: "admin",
                defaults: new { controller = "Admin", action = "ManagerPage" }
            );
            routes.MapRoute(
                name: "SetEventChecked",
                url: "admin/events/setchecked/{id}",
                defaults: new { controller = "Admin", action = "ToggleButtonStatusChecked" }
            );
            routes.MapRoute(
                name: "ToogleIsActiveEvent",
                url: "admin/events/toggleactive",
                defaults: new { controller = "Admin", action = "ToggleButtonStatusActive" }
            );
            routes.MapRoute(
               name: "getEventsToAdminPage",
               url: "admin/events/getall",
               defaults: new { controller = "Admin", action = "GetEvents" }
           );
            routes.MapRoute(
                name: "MarkEvent",
                url: "admin/events/mark",
                defaults: new { controller = "Admin", action = "MarkEvent" }
            );
            routes.MapRoute(
                name: "DeleteEvent",
                url: "admin/events/delete",
                defaults: new { controller = "Admin", action = "DeleteEvent" }
            );

            #endregion

            #region Subscribers map routes
            routes.MapRoute(
                name: "Subscribers",
                url: "subscribers/{id}",
                defaults: new { controller = "Subscribers", action = "GetSubscribers" }
            );
            routes.MapRoute(
                name: "CountPost",
                url: "subscribers/getcount",
                defaults: new { controller = "Subscribers", action = "GetCount" }
            );
            routes.MapRoute(
                name: "CountGet",
                url: "subscribers/getcount/{id}",
                defaults: new { controller = "Subscribers", action = "Index" }
            );
            routes.MapRoute(
                name: "Subscribe",
                url: "subscribe",
                defaults: new { controller = "Subscribers", action = "Subscribe" }
            );
            routes.MapRoute(
                name: "Unsubscribe",
                url: "unsubscribe",
                defaults: new { controller = "Subscribers", action = "Unsubscribe" }
            );
            routes.MapRoute(
                name: "IsSubscribed",
                url: "issubscribed",
                defaults: new { controller = "Subscribers", action = "IsSubscribed" }
            );

            #endregion

            routes.MapRoute(
                "NotFound",
                "{*url}",
             new { controller = "Error", action = "Index" }
            );
        }
    }
}