﻿using Events.Business.Classes;
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
            /*var provider = new NHibernateSubscribersDataProvider();
            provider.SubscribeUser(new Subscribing("99974bdc-e120-4061-ba54-5b9474c87129", "a0c6a802-7dfd-4490-9450-84488396538f"));*/
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
            var evntModel = eventManager.GetById(id);

            if (evntModel == null)
            {
                return View("EventNotFound");
            }

            var evntViewModel = AutoMapper.Mapper.Map<EventViewModel>(evntModel);
            ViewData["Comments"] = commentManager.GetByEventId(id);
            return View(evntViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
           // ViewBag.cities = cityManager.GetList();
            ViewBag.Title = "Создайте собственное событие";
                var evnt = new EventViewModel();
            return View(evnt);
        }
        [HttpGet]
        public ActionResult Update(string id)
        {
            var evntModel = eventManager.GetById(id);
            if (evntModel == null)
            {
                return View("EventNotFound");
            }
            var evnt = AutoMapper.Mapper.Map<EventViewModel>(evntModel);
            ViewBag.Title = "Редактируйте своё событие";
            return View("Create", evnt);
        }

        [HttpPost]
        public ActionResult Create(EventViewModel evnt)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Title = "Создайте собственное событие";
                return View(evnt);
            }

            var evntModel = AutoMapper.Mapper.Map<Event>(evnt);
            eventManager.Create(evntModel.Id, evntModel);
            return RedirectToAction("Index");
        }
    }
}
