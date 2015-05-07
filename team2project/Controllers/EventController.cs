using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using team2project.Models;
using System.Globalization;
using System.Web.Routing;

namespace team2project.Controllers
{
    public class EventController : Controller
    {
        //
        // GET: /Event/

        public ActionResult Index()
        {
            var model = new DataProvider().GetEvents();
            return View("~/Views/Event/List.cshtml", model);
        }

        // GET: /Home/List
        [HttpGet]
        public ActionResult Details(uint id)
        {
            var model = new DataProvider().GetById(id);
            if (model == null)
            {
                return RedirectToAction("Index", new { controller = "Error", action = "Index" });
            }
            return View(model);
        }

    }
}
