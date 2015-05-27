using Events.Business.Classes;
using Events.Business.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using team2project.Models;
using System.Web.Helpers;
using Events.NHibernateDataProvider.NHibernateCore;
using System.Web.Script.Serialization;

namespace team2project.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        EventManager manager;
        CommentManager commentManager;

        public AdminController(EventManager manager, CommentManager commentManager)
        {
            this.manager = manager;
            this.commentManager = commentManager;
        }

        [HttpGet]
        public ActionResult ManagerPage()
        {
            List<EventViewModel> list = AutoMapper.Mapper.Map<List<EventViewModel>>(manager.GetAllEvents());
            return View("ManagerPage", list);
        }

        [HttpGet]
        public string GetEvents()
        {
            List<EventViewModel> list = AutoMapper.Mapper.Map<List<EventViewModel>>(manager.GetAllEvents());
            return new JavaScriptSerializer().Serialize(list);
        }

        [HttpGet]
        public string ToggleButtonStatusActive(string id)
        {
            manager.ToggleButtonStatusActive(id);
            return "ok";
        }

        [HttpGet]
        public string ToggleButtonStatusChecked(string id)
        {
            manager.ToggleButtonStatusChecked(id);
            return "ok";
        }

        [HttpGet]
        public string DeleteEvent(string id)
        {
            commentManager.DeleteByEventId(id);
            manager.Delete(id);
            return "ok";
        }

    }
}
