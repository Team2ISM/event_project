using Events.Business;
using Events.Business.Classes;
using Events.Business.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace team2project.Components
{
    public class WebUserContext : UserContext
    {
        private bool IsPrincipalAvailable()
        {
            if (HttpContext.Current == null)
                return false;

            var principal = HttpContext.Current.User;
            if (principal == null)
                return false;

            if (principal.Identity == null)
                return false;

            return true;
        }

        private IPrincipal WebUser
        {
            get
            {
                if (!IsPrincipalAvailable())
                    return null;

                return HttpContext.Current.User;
            }
        }

        public override bool IsLoggedIn
        {
            get
            {
                return IsPrincipalAvailable() && WebUser.Identity.IsAuthenticated;
            }
        }

        public override bool IsAdmin
        {
            get
            {
                return IsPrincipalAvailable() && WebUser.IsInRole("Admin");
            }
        }

        public override User User
        {
            get
            {
                if (!IsLoggedIn)
                    return null;

                return DependencyResolver.Current.GetService<UserManager>().GetByEmail(WebUser.Identity.Name);
            }
        }
    }
}