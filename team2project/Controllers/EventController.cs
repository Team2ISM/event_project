using Events.Business.Classes;
using Events.Business.Models;
using Events.NHibernateDataProvider.NHibernateCore;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using team2project.Models;

namespace team2project.Controllers
{
    public class EventController : Controller
    {
        EventManager eventManager;
        CommentManager commentManager;
        CitiesManager cityManager;

        public EventController(EventManager eventManager, CommentManager commentManager, CitiesManager cityManager)
        {
            this.eventManager = eventManager;
            this.commentManager = commentManager;
            this.cityManager = cityManager;
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
            if (days == "-1")
            {
                return View("List", AutoMapper.Mapper.Map<List<EventViewModel>>(eventManager.GetList(loc, null)));
            }
            List<EventViewModel> list = AutoMapper.Mapper.Map<List<EventViewModel>>(eventManager.GetList(loc, days));
            return View("List", list);
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            var evntModel = eventManager.GetById(id);

            if (evntModel == null)
            {
                return View("EventNotFound");
            }
            if(evntModel.AuthorId=="undefinded")
            {
                return View("~/Views/Error/Page404.cshtml");
            }
            var evntViewModel = AutoMapper.Mapper.Map<EventViewModel>(evntModel);
            ViewData["Comments"] = commentManager.GetByEventId(id);
            return View(evntViewModel);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
           // ViewBag.cities = cityManager.GetList();
            ViewBag.Title = "Создайте собственное событие";
            ViewBag.Button = "Создать";
                var evnt = new EventViewModel();
            return View(evnt);
        }
        [HttpGet]
        [Authorize]
        public ActionResult Update(string id)
        {
            var evntModel = eventManager.GetById(id);
            if (evntModel == null)
            {
                return View("EventNotFound");
            }
            var evnt = AutoMapper.Mapper.Map<EventViewModel>(evntModel);
            ViewBag.Title = "Редактируйте это событие";
            ViewBag.Button = "Сохранить";
            return View("Create", evnt);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Update(EventViewModel evnt)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Title = "Редактируйте это событие";
                ViewBag.Button = "Сохранить";
                return View("Create", evnt);
            }
            evnt.TextDescription = evnt.TextDescription.Substring(0, evnt.TextDescription.Length < 51 ? evnt.TextDescription.Length - 1 : 50);
            evnt.AuthorId = User.Identity.Name;
            var evntModel = AutoMapper.Mapper.Map<Event>(evnt);
            eventManager.Update(evntModel);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public ActionResult DeleteEvent(string id)
        {
            commentManager.DeleteByEventId(id);
            eventManager.Delete(id);
            return RedirectToRoute("FutureEvents");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(EventViewModel evnt)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Title = "Создайте собственное событие";
                ViewBag.Button = "Создать";
                return View(evnt);
            }
            evnt.TextDescription = evnt.TextDescription.Substring(0, evnt.TextDescription.Length<51 ? evnt.TextDescription.Length-1 : 50);
            var evntModel = AutoMapper.Mapper.Map<Event>(evnt);
            evntModel.AuthorId = User.Identity.Name;
            eventManager.Create(evntModel.Id, evntModel);
            return RedirectToAction("Index");
        }

    }
}
