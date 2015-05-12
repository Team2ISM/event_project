using System;
using System.Web.Mvc;
using team2project.Models;
using DAL.Models;
using BLL.Classes;

namespace team2project.Controllers
{
    public class EventController : Controller
    {
        BusinessLogicLayer<EventViewModel, EventModel> BusinesLogicLayer;

        [HttpGet]
        public ActionResult Index()
        {
            BusinesLogicLayer = new BusinessLogicLayer<EventViewModel, EventModel>();
            return View("List", BusinesLogicLayer.GetList());
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            BusinesLogicLayer = new BusinessLogicLayer<EventViewModel, EventModel>();

            var evnt = BusinesLogicLayer.GetById(id);
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
            try
            {
                BusinesLogicLayer = new BusinessLogicLayer<Models.EventViewModel, EventModel>();
                BusinesLogicLayer.Create(evnt);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
