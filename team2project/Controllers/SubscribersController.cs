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
        SubscribersManager manager;
        UserManager userManager;

        public SubscribersController(SubscribersManager subscribersManager, UserManager userManager)
        {
            this.manager = subscribersManager;
            this.userManager = userManager;
        }

        [HttpGet]
        public ActionResult Index(string id)
        {
            return View(manager.GetCount(id));
        }

        [HttpPost]
        public ActionResult GetCount(string id)
        {
            return Json(manager.GetCount(id));
        }

        public ActionResult GetSubscribers(string id)
        {
            return Json(AutoMapper.Mapper.Map<List<Subscriber>>(manager.GetAllSubscribers(id)));
        }

        public ActionResult Subscribe(string id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(false);
            }
            var user = userManager.GetByEmail(User.Identity.Name);
            manager.SubscribeUser(new Subscribing(id, user.Id));
            return Json(true);
        }

        public ActionResult Unsubscribe(string id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(false);
            }
            var user = userManager.GetByEmail(User.Identity.Name);
            manager.UnsubscribeUser(new Subscribing(id, user.Id));
            return Json(true);
        }

        public ActionResult IsSubscribed(string id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(false);
            }
            var user = userManager.GetByEmail(User.Identity.Name);
            return Json(manager.IsSubscribed(new Subscribing(id, user.Id)));
        }
    }
}
