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
using team2project.Properties;

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
            PrepareEventsToView(ref list);
            ViewBag.location = location;
            return View("List", list);
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            var evntModel = eventManager.GetById(id);
            if (evntModel == null || !evntModel.Active)
            {
                return View("GenericError", model: Resources.ResponseEventNotFound);
            }
            var evntViewModel = AutoMapper.Mapper.Map<EventViewModel>(evntModel);
            evntViewModel.Location = cityManager.GetById(evntViewModel.LocationId).Name;
            evntViewModel.Comments = commentManager.GetByEventId(id);
            return View(evntViewModel);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create(string returnUrl)
        {
            var evnt = new EventViewModel();
            ViewBag.returnUrl = (String.IsNullOrEmpty(returnUrl)) ? "/events/all" : returnUrl;
            return View(evnt);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Update(string id, string returnUrl)
        {
            var evntModel = eventManager.GetById(id);
            if (evntModel == null)
            {
                return View("GenericError", model: Resources.ResponseEventNotFound);
            }
            if (DateTime.Now > evntModel.ToDate)
            {
                return View("GenericError", model: Resources.ResponseEditingNotAllowedDueToEventEndingTime);
            }
            if (evntModel.AuthorId != User.Identity.Name)
            {
                return View("GenericError", model: Resources.ResponseEditingNotAllowedDueToWrongUser);
            }
            var evnt = AutoMapper.Mapper.Map<EventViewModel>(evntModel);
            ViewBag.returnUrl = (String.IsNullOrEmpty(returnUrl)) ? "/events/all" : returnUrl;
            return View("Create", evnt);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Update(EventViewModel evnt)
        {
            if (!ModelState.IsValid) return View("Create", evnt);

            if (evnt.AuthorId != User.Identity.Name)
            {
                return View("GenericError", model: Resources.ResponseEditingNotAllowedDueToWrongUser);
            }            
            evnt.AuthorId = User.Identity.Name;
            var evntModel = AutoMapper.Mapper.Map<Event>(evnt);
            if (!String.IsNullOrEmpty(evnt.Location))
            {
                evntModel.LocationId = cityManager.GetByName(evnt.Location).Id;
            }
            evntModel.Description = evntModel.Description.RemovePreTag();
            evntModel.DateOfCreation = eventManager.GetById(evntModel.Id).DateOfCreation;
            eventManager.Update(evntModel);
            return RedirectToRoute("EventDetails", new { id = evntModel.Id });
        }

        [HttpPost]
        [Authorize]
        public ActionResult DeleteEvent(string id)
        {
            var mail = User.Identity.Name;
            var target = eventManager.GetById(id);
            if (target == null)
            {
                return View("GenericError", model: Resources.ResponseEventNotFound);
            }
            if (mail != target.AuthorId)
            {
                return View("GenericError", model: Resources.ResponseDeletingNotAllowedDueToWrongUser);
            }
            eventManager.Delete(id);
            return RedirectToRoute("FutureEvents");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(EventViewModel evnt)
        {
            if (!ModelState.IsValid) return View(evnt);

            var evntModel = AutoMapper.Mapper.Map<Event>(evnt);
            evntModel.LocationId = cityManager.GetByName(evnt.Location).Id;
            evntModel.AuthorId = User.Identity.Name;
            evntModel.Description = evntModel.Description.RemovePreTag();
            eventManager.Create(evntModel);
            return RedirectToRoute("EventDetails", new { id = evntModel.Id});
        }

        [Authorize]
        [HttpGet]
        public ActionResult MyPastEvents()
        {
            IList<Event> events = eventManager.GetAuthorPastEvents(User.Identity.Name);
            List<EventViewModel> eventsModels = AutoMapper.Mapper.Map<List<EventViewModel>>(events);
            PrepareEventsToView(ref eventsModels);
            return View(eventsModels);
        }

        [Authorize]
        [HttpGet]
        public ActionResult MyFutureEvents()
        {
            IList<Event> events = eventManager.GetAuthorFutureEvents(User.Identity.Name);
            List<EventViewModel> eventsModels = AutoMapper.Mapper.Map<List<EventViewModel>>(events);
            PrepareEventsToView(ref eventsModels);
            return View(eventsModels);
        }

        private void PrepareEventsToView(ref List<EventViewModel> eventsModels)
        {
            foreach (var ev in eventsModels)
            {
                ev.Location = cityManager.GetById(Convert.ToInt32(ev.LocationId)).Name;
            }
        }
    }
}