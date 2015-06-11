using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using team2project.Models;
using System.Globalization;
using team2project.Helpers;
using Events.Business.Helpers;

namespace team2project.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToRoute("EventsList", routeValues: new { period = PeriodStates.Anytime });
        }
    }
}
