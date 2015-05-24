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
        public ISubscribersDataProvider provider;
        public SubscribersController( ) {
            provider = new NHibernateSubscribersDataProvider();
        }

        [HttpGet]
        public ActionResult Index(string EventId)
        {
            return View(provider.GetCount(EventId));
        }
    }
}
