using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using team2project.Models;
using System.Globalization;

namespace team2project.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Home/List
        [HttpGet]
        public ActionResult List()
        {
            var model = new DataProvider().GetEvents();
            return View(model);
        }

        // GET: /Home/List
        [HttpGet]
        public ActionResult Details(uint id)
        {
            var model = new DataProvider().GetById(id);
            if (id > 10)
                return HttpNotFound();
            return View(model);
        }
        [HttpGet]
        public ActionResult Page404()
        {
            return View();
        }
        protected override void HandleUnknownAction(string actionName) {
            var result = new ViewResult { ViewName = "Page404" };
            result.ExecuteResult(ControllerContext);
        }
    }
}
