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
            ViewBag.list = homeModel.GetEvents();
            return View();
        }

        // GET: /Home/List
        [HttpGet]
        public ActionResult Details(uint id, HomeModel homeModel)
        {
            ViewBag.curEvent = homeModel.GetById(id);
            return View();
        }
    }
}
