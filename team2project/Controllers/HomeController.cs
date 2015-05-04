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
        public ActionResult List(HomeModel homeModel)
        {
            //ViewBag.list = homeModel.GetEvents();
            return View(homeModel.GetEvents());
        }

        // GET: /Home/List
        [HttpGet]
        public ActionResult Details(uint id, HomeModel homeModel)
        {            
            return View(homeModel.GetById(id));
        }
    }
}
