using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using team2project.Models;
using System.Globalization;

using Events.Business.Models;
using Events.Business.Interfaces;
using Events.NHibernateDataProvider.NHibernateCore;

namespace team2project.Controllers
{
    public class SubscribersController : Controller
    {
        public ISubscribersDataProvider Provider;
        public SubscribersController( ) {
            Provider = new NHibernateSubscribersDataProvider();
        }

        public ActionResult Index(string EventId)
        {
            @ViewBag.EventId = EventId;
            return View(Provider.GetCount(EventId));
        }

        public JsonResult GetSubscribers(string id) {
            return Json(Provider.GetAllSubscribers(id), JsonRequestBehavior.AllowGet);

        }

        public JsonResult Subscribe(string id) {
            if (String.IsNullOrEmpty(User.Identity.Name)) return Json(false);
            var user = new NHibernateUserDataProvider().GetByMail(User.Identity.Name);      
            Provider.SubscribeUser(new Subscribing(id, user.Id));
            return Json(true);
        }
        public JsonResult Unsubscribe(string id) {
            if (String.IsNullOrEmpty(User.Identity.Name)) return Json(false);
            var user = new NHibernateUserDataProvider().GetByMail(User.Identity.Name);
            Provider.UnsubscribeUser(new Subscribing(id, user.Id));
            return Json(true);
        }
    }
}
