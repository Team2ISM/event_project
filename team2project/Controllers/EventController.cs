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
            if (evntModel == null || !evntModel.Active)
            {
                return View("GenericError", ResponseMessages.EventNotFound);
            }
            var evntViewModel = AutoMapper.Mapper.Map<EventViewModel>(evntModel);
            evntViewModel.Location = cityManager.GetById(evntViewModel.LocationId).Name;
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
            if (evntModel == null)
            {
                return View("GenericError", ResponseMessages.EventNotFound);
            }
            if (DateTime.Now > evntModel.ToDate)
            {
                return View("GenericError", ResponseMessages.EditingNotAllowedDueToEventEndingTime);
            }
            if (evntModel.AuthorId != User.Identity.Name)
            {
                return View("GenericError", ResponseMessages.EditingNotAllowedDueToWrongUser);
            }
            var evnt = AutoMapper.Mapper.Map<EventViewModel>(evntModel);
            return View("Create", evnt);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Update(EventViewModel evnt)
        {
            evnt = evnt.Merge(eventManager.GetById(evnt.Id));
            if (evnt.AuthorId != User.Identity.Name)
            {
                return View("GenericError", ResponseMessages.EditingNotAllowedDueToWrongUser);
            }
            if (!ModelState.IsValid)
            {
                return View("Create", evnt);
            }
            evnt.AuthorId = User.Identity.Name;
            var evntModel = AutoMapper.Mapper.Map<Event>(evnt);
            if (!String.IsNullOrEmpty(evnt.Location))
            {
                evntModel.LocationId = cityManager.GetByName(evnt.Location).Id;
            }
            evntModel.Description = evntModel.Description.RemovePreTag();
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
                return View("GenericError", ResponseMessages.EventNotFound);
            }
            if (mail != target.AuthorId)
            {
                return View("GenericError", ResponseMessages.DeletingNotAllowedDueToWrongUser);
            }
            eventManager.Delete(id);
            return RedirectToRoute("FutureEvents");
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
            evntModel.Description = evntModel.Description.RemovePreTag();
            eventManager.Create(evntModel.Id, evntModel);
            return RedirectToRoute("EventDetails", new { id = evntModel.Id});
        }

        [Authorize]
        [HttpGet]
        public ActionResult MyPastEvents()
        {
            IList<Event> events = eventManager.GetAuthorPastEvents(User.Identity.Name);
            List<EventViewModel> eventsModels = AutoMapper.Mapper.Map<List<EventViewModel>>(events);
            foreach (var ev in eventsModels)
            {
                ev.Location = cityManager.GetById(Convert.ToInt32(ev.LocationId)).Name;
            }
            return View(eventsModels);
        }

        [Authorize]
        [HttpGet]
        public ActionResult MyFutureEvents()
        {
            IList<Event> events = eventManager.GetAuthorFutureEvents(User.Identity.Name);
            List<EventViewModel> eventsModels = AutoMapper.Mapper.Map<List<EventViewModel>>(events);
            foreach (var ev in eventsModels)
            {
                ev.Location = cityManager.GetById(Convert.ToInt32(ev.LocationId)).Name;
            }
            return View(eventsModels);
        }

    }
}