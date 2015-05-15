using System;
using System.Web.Mvc;
using team2project.Models;
using BLL.Models;
using BLL.Classes;
namespace team2project.Controllers
{
    public class EventController : Controller
    {

        EventsBusiness<EventViewModel, EventModel> Business;

        public EventController(EventsBusiness<EventViewModel, EventModel> business)
        {
            Business = business;
        }

         [HttpGet]
        public ActionResult Index()
        {
            return View("List", Business.GetList());
        }

         [HttpGet]
         public ActionResult Details(string id)
         {
             var evnt = Business.GetById(id);
             if (evnt == null)
             {
                 return View("EventNotFound");
             }

             return View(evnt);
         }

         [HttpGet]
         public ActionResult Create()
         {
             var evnt = new EventViewModel();
             return View(evnt);
         }

         [HttpPost]
         public ActionResult Create(EventViewModel evnt)
         {
             if (!ModelState.IsValid) return View(evnt);
             try
             {
                 Business.Create(evnt.Id, evnt);
                 return RedirectToAction("Index");
             }
             catch
             {
                 return View();
             }
         }
    }
}
