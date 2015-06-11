using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using team2project.Models;
using System.Globalization;
using Events.Business.Models;
using Events.Business.Classes;

namespace team2project.Controllers
{
    public class SubscribersController : Controller
    {
        SubscribersManager Manager;
        UserManager UserManager;

        public SubscribersController(SubscribersManager subscribersManager, UserManager userManager)
        {
            Manager = subscribersManager;
            UserManager = userManager;
        }

        [HttpGet]
        public ActionResult Index(string EventId)
        {
            return View(Manager.GetCount(EventId));
        }

        [HttpPost]
        public JsonResult GetCount(string id)
        {
            return Json(Manager.GetCount(id));
        }

        public JsonResult GetSubscribers(string id)
        {
            return Json(Manager.GetAllSubscribers(id));
        }

        public JsonResult Subscribe(string id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(false);
            }
            var user = UserManager.GetByEmail(User.Identity.Name);      
            Manager.SubscribeUser(new Subscribing(id, user.Id));
            return Json(true);
        }

        public JsonResult Unsubscribe(string id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(false);
            }
            var user = UserManager.GetByEmail(User.Identity.Name);
            Manager.UnsubscribeUser(new Subscribing(id, user.Id));
            return Json(true);
        }

        public JsonResult IsSubscribed(string id)
        {
            if (String.IsNullOrEmpty(User.Identity.Name))
            {
                return Json(false);
            }
            var user = UserManager.GetByEmail(User.Identity.Name);
            return Json(Manager.IsSubscribed(new Subscribing(id, user.Id)));      
        }
    }
}
