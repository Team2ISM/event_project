using System;
using System.Web.Mvc;
using team2project.Models;
using DAL.Models;
using BLL.Classes;

namespace team2project.Controllers
{
    public class EventController : Controller
    {
        BusinessLogicLayer<EventViewModel, EventModel> Bll;

        [HttpGet]
        public ActionResult Index()
        {
            Bll = new BusinessLogicLayer<EventViewModel, EventModel>();
            return View("List", Bll.GetList());
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            Bll = new BusinessLogicLayer<EventViewModel, EventModel>();

            var Event = Bll.GetById(id);
            if (Event == null)
            {
                return View("EventNotFound");
            }

            return View(Event);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var Event = new EventViewModel();
            return View(Event);
        }

        [HttpPost]
        public ActionResult Create(EventViewModel evnt)
        {
            try
            {
                evnt.Id = Guid.NewGuid().ToString();
                Bll = new BusinessLogicLayer<Models.EventViewModel, EventModel>();
                Bll.Create(evnt);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
