using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using team2project.Models;

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
        public ActionResult List(HomeModel homeModel)
        {
            var model = homeModel;
            ViewBag.list = model.GetEvents();
            return View();
        }

        // GET: /Home/List
        [HttpGet]
        public ActionResult Details(uint id, HomeModel homeModel)
        {
            var model = homeModel;
            ViewBag.curEvent = model.GetById(id);
            return View();
        }
    }
}
