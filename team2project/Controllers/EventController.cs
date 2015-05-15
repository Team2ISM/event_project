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
            var currentEvent = Business.GetById(id);
            if (currentEvent == null)
            {
                return View("EventNotFound");
            }

            return View(currentEvent);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var newEvent = new EventViewModel();
            return View(newEvent);
        }

        [HttpPost]
        public ActionResult Create(EventViewModel newEvent)
        {
            if (!ModelState.IsValid) return View(newEvent);
            try
            {
                Business.Create(newEvent.Id, newEvent);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
