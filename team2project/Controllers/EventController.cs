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
using System.Linq;

namespace team2project.Controllers
{
    public class EventController : Controller
    {
	EventManager eventManager;
        CommentManager commentManager;
        CitiesManager cityManager;
        UserManager userManager;
        SubscribersManager subscribersManager;

        public EventController(EventManager eventManager, CommentManager commentManager, CitiesManager cityManager, UserManager userManager, SubscribersManager subscribersManager)
        {
	    this.eventManager = eventManager;
            this.commentManager = commentManager;
            this.cityManager = cityManager;
            this.userManager = userManager;
            this.subscribersManager = subscribersManager;
        }

        [HttpGet]
        public ActionResult Index(string period, string location)
        {
            try
            {
                var listModel = eventManager.GetList(period, location);
                var listModel = new EventListViewModel(AutoMapper.Mapper.Map<List<EventViewModel>>(list), location);
            listModel.PrepareEventsToView(cityManager);
            return View("List", listModel);
            }
            catch (ArgumentException ex)

            {	
		return View("GenericError", model: Resources.ListOfEventsNotFound);
            }
       }

        [HttpGet]
        public ActionResult Details(string id)
        {
            var eventsBussinesModelList = eventManager.GetById(id);
            if (eventsBussinesModelList == null || !eventsBussinesModelList.Active)
            {
                return View("GenericError", model: Resources.ResponseEventNotFound);
            }
            var eventsViewModelList = AutoMapper.Mapper.Map<EventViewModel>(eventsBussinesModelList);
            eventsViewModelList.Location = cityManager.GetById(eventsViewModelList.LocationId).Name;
            eventsViewModelList.Comments = commentManager.GetByEventId(id);
            return View(eventsViewModelList);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create(string returnUrl)
        {
            ViewBag.returnUrl = (String.IsNullOrEmpty(returnUrl)) ? "/events/all" : returnUrl;
            return View(new EventViewModel());
        }

        [HttpGet]
        [Authorize]
        public ActionResult Update(string id, string returnUrl)
        {
            var eventBussinesModel = eventManager.GetById(id);
            if (eventBussinesModel == null)
            {
                return View("GenericError", model: Resources.ResponseEventNotFound);
            }
            if (DateTime.Now > eventBussinesModel.ToDate)
            {
                return View("GenericError", model: Resources.ResponseEditingNotAllowedDueToEventEndingTime);
            }
            if (eventBussinesModel.AuthorId != User.Identity.Name)
            {
                return View("GenericError", model: Resources.ResponseEditingNotAllowedDueToWrongUser);
            }
            var eventModel = AutoMapper.Mapper.Map<EventViewModel>(eventBussinesModel);
            ViewBag.returnUrl = (String.IsNullOrEmpty(returnUrl)) ? "/events/all" : returnUrl;
            return View("Create", eventModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Update(EventViewModel eventModel)
        {
            if (!ModelState.IsValid) return View("Create", eventModel);

            if (eventModel.AuthorId != User.Identity.Name)
            {
                return View("GenericError", model: Resources.ResponseEditingNotAllowedDueToWrongUser);
            }            
            eventModel.AuthorId = User.Identity.Name;
            var eventBussinesModel = AutoMapper.Mapper.Map<Event>(eventModel);
            eventBussinesModel.Description = eventBussinesModel.Description.RemovePreTag();
            eventBussinesModel.DateOfCreation = eventManager.GetById(eventBussinesModel.Id).DateOfCreation;
            eventManager.Update(eventBussinesModel);
            return RedirectToRoute("EventDetails", new { id = eventBussinesModel.Id });

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
        public ActionResult Create(EventViewModel eventModel)
        {
            if (!ModelState.IsValid) return View(eventModel);
            var eventBussinesModel = AutoMapper.Mapper.Map<Event>(eventModel);
            eventBussinesModel.AuthorId = User.Identity.Name;
            eventBussinesModel.Description = eventBussinesModel.Description.RemovePreTag();
            eventManager.Create(eventBussinesModel);
            return RedirectToRoute("EventDetails", new { id = eventBussinesModel.Id});

        }

        [Authorize]
        [HttpGet]
        public ActionResult MyPastEvents()
        {
            var user = userManager.GetByEmail(User.Identity.Name);

            var eventsId = subscribersManager.GetMyEventsId(user.Id);
            IList<Event> events = eventManager.GetMyPastEvents(eventsId);
            var eventsListModel = new EventListViewModel(AutoMapper.Mapper.Map<List<EventViewModel>>(events));
            eventsListModel.EventsList = eventsListModel.EventsList.OrderBy(x => x.FromDate).ToList();
            eventsListModel.PrepareEventsToView(cityManager);
            return View(eventsListModel);

        }

        [Authorize]
        [HttpGet]
        public ActionResult MyFutureEvents()
        {
            var user = userManager.GetByEmail(User.Identity.Name);

            var eventsId = subscribersManager.GetMyEventsId(user.Id);
            IList<Event> events = eventManager.GetMyFutureEvents(eventsId);
            var eventsListModel = new EventListViewModel(AutoMapper.Mapper.Map<List<EventViewModel>>(events));
            eventsListModel.EventsList = eventsListModel.EventsList.OrderBy(x => x.FromDate).ToList();
            eventsListModel.PrepareEventsToView(cityManager);
            return View(eventsListModel);

        }
    }
    
    static class Extension
    {
        public static void PrepareEventsToView(this EventListViewModel Model, CitiesManager cityManager)
        {
            foreach (var eventModel in Model.EventsList)
            {
                eventModel.Location = cityManager.GetById(eventModel.LocationId).Name;
            }
        }
    }
}