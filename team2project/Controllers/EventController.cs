using Events.Business.Classes;
using Events.Business.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using team2project.Models;

namespace team2project.Controllers
{
    public class EventController : Controller
    {

        EventManager manager;
        public EventController(EventManager manager)
        {
            this.manager = manager;
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<EventViewModel> list = AutoMapper.Mapper.Map<List<EventViewModel>>(manager.GetList());
            return View("List", list);
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            var evnt = AutoMapper.Mapper.Map<EventViewModel>(manager.GetById(id));

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
            if (!ModelState.IsValid || evnt.To <= evnt.From)
            {
                return View(evnt);
            }
            
            var evntModel = AutoMapper.Mapper.Map<Event>(evnt);
            manager.Create(evntModel.Id, evntModel);
            return RedirectToAction("Index");
        }
    }
}
