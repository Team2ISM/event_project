using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace team2project.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/
        [HttpGet]
        public ActionResult Index()
        {
            Response.StatusCode = 404;
            return View("~/Views/Error/Page404.cshtml");
        }

    }
}
