using Events.Business.Classes;
using Events.Business;
using Events.Business.Models;
using Events.NHibernateDataProvider.NHibernateCore;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using team2project.Models;
using team2project.Helpers;


namespace team2project.Controllers
{
    public class EventController : Controller
    {
        EventManager eventManager;
        CommentManager commentManager;
        CitiesManager cityManager;
        UserManager userManager;

        public EventController(EventManager eventManager, CommentManager commentManager, CitiesManager cityManager, UserManager userManager)
        {
            this.eventManager = eventManager;
            this.commentManager = commentManager;
            this.cityManager = cityManager;
            this.userManager = userManager;
        }

        [HttpGet]
        public ActionResult Index(string period, string location)
        {
            var listModel = eventManager.GetList(period, location);
            if (listModel == null)
            {
                return RedirectToRoute("Error404");
            }
            List<EventViewModel> list = AutoMapper.Mapper.Map<List<EventViewModel>>(listModel);
            ViewBag.city = null;

            if (!string.IsNullOrEmpty(location))
            {
                ViewBag.city = location;
            }

            int? locId = null;
            //if (!string.IsNullOrEmpty(location))
            //    locId = cityManager.GetByName(location).Id;
            foreach (var ev in list)
            {
                ev.Location = cityManager.GetById(ev.LocationId).Name;
            }
            return View("List", list);
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            var evntModel = eventManager.GetById(id);

            if (evntModel == null)
            {
                return View("~/Views/Error/Page404.cshtml");
            }
            if (evntModel.Active == false)
            {
                return View("EventNotFound");
            }
            var evntViewModel = AutoMapper.Mapper.Map<EventViewModel>(evntModel);
            evntViewModel.Comments = commentManager.GetByEventId(id);
            return View(evntViewModel);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            var evnt = new EventViewModel();
            return View(evnt);
        }
        [HttpGet]
        [Authorize]
        public ActionResult Update(string id)
        {
            var evntModel = eventManager.GetById(id);

            if (evntModel.AuthorId != User.Identity.Name || DateTime.Now > evntModel.DateOfCreation)
                return RedirectToAction("Index");

            if (evntModel == null)
            {
                return View("EventNotFound");
            }
            var evnt = AutoMapper.Mapper.Map<EventViewModel>(evntModel);
            return View("Create", evnt);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Update(EventViewModel evnt)
        {

            if (evnt.AuthorId != User.Identity.Name)
                return RedirectToAction("Index");

            if (!ModelState.IsValid)
            {
                return View("Create", evnt);
            }
            evnt.AuthorId = User.Identity.Name;
            var evntModel = AutoMapper.Mapper.Map<Event>(evnt);
            evntModel.LocationId = cityManager.GetByName(evnt.Location).Id;
            // Replace <pre> tags with nothing, 'cause they break markup
            evntModel.Description = evntModel.Description.Replace("<pre>", "");
            evntModel.Description = evntModel.Description.Replace("</pre>", "");
            //
            eventManager.Update(evntModel);
            return RedirectToRoute("Home");
        }

        [HttpGet]
        [Authorize]
        public ActionResult DeleteEvent(string id)
        {
            var mail = User.Identity.Name;
            User user = userManager.GetByMail(mail);
            if (user.Id == eventManager.GetById(id).AuthorId)
            {
                eventManager.Delete(id);
                return RedirectToRoute("FutureEvents");
            }
            else return RedirectToRoute("Home");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(EventViewModel evnt)
        {
            if (!ModelState.IsValid)
            {
                return View(evnt);
            }
            var evntModel = AutoMapper.Mapper.Map<Event>(evnt);
            evntModel.LocationId = cityManager.GetByName(evnt.Location).Id;
            evntModel.AuthorId = User.Identity.Name;

            // Replace <pre> tags with nothing, 'cause they break markup
            evntModel.Description = evntModel.Description.Replace("<pre>", "");
            evntModel.Description = evntModel.Description.Replace("</pre>", "");
            //

            eventManager.Create(evntModel.Id, evntModel);
            return RedirectToRoute("Home");
        }

    }
}