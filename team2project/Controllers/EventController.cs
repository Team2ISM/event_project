using System.Web.Script.Serialization;
using Events.Business.Classes;
using Events.Business.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using Comments.Business.Classes;
using team2project.Models;

namespace team2project.Controllers
{
    public class EventController : Controller
    {
        EventManager eventManager;
        CommentManager commentManager;

        public EventController(EventManager eventManager, CommentManager commentManager)
        {
            this.eventManager = eventManager;
            this.commentManager = commentManager;
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<EventViewModel> list = AutoMapper.Mapper.Map<List<EventViewModel>>(eventManager.GetList());
            return View("List", list);
        }

        [HttpGet]
        public ActionResult Filters(string loc, string days)
        {
            List<EventViewModel> list = AutoMapper.Mapper.Map<List<EventViewModel>>(eventManager.GetList(loc, days));
            return View("List", list);
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            var evnt = AutoMapper.Mapper.Map<EventViewModel>(eventManager.GetById(id));

            if (evnt == null)
            {
                return View("EventNotFound");
            }
            ViewData["Comments"] = commentManager.GetByEventId(id);
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
            if (!ModelState.IsValid || evnt.FromDate >= evnt.ToDate)
            {
                return View(evnt);
            }

            var evntModel = AutoMapper.Mapper.Map<Event>(evnt);
            eventManager.Create(evntModel.Id, evntModel);
            return RedirectToAction("Index");
        }
    }
}
