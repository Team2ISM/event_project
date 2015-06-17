using Events.Business.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Events.Business
{
    public static class Api
    {
        public static CommentManager CommentManager
        {
            get { return DependencyResolver.Current.GetService<CommentManager>(); }
        }

        public static RemindManager RemindManager
        {
            get { return DependencyResolver.Current.GetService<RemindManager>(); }
        }

        public static CitiesManager CitiesManager 
        {
            get { return DependencyResolver.Current.GetService<CitiesManager>(); }
        }

        public static UserManager UserManager 
        {
            get { return DependencyResolver.Current.GetService<UserManager>(); }
        }
        public static SubscribersManager SubscribersManager
        {
            get { return DependencyResolver.Current.GetService<SubscribersManager>(); }
        }
    }
}
