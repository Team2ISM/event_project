using Events.Business.Classes;
using Events.Business.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using team2project.Models;
using System.Web.Helpers;
using Events.NHibernateDataProvider.NHibernateCore;

namespace team2project.Controllers
{
    [Authorize(Roles = "Admin")]
    //[Authorize(Users="team2project222@gmail.com")]
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

        public ActionResult ToggleButtonStatusActive(string id)
        {
            manager.ToggleButtonStatusActive(id);
            return RedirectToRoute("ManagerPage");
        }

        public void ToggleButtonStatusChecked(string id)
        {
            manager.ToggleButtonStatusChecked(id);
        }

        public ActionResult DeleteEvent(string id)
        {
            commentManager.DeleteByEventId(id);
            manager.Delete(id);            
            return RedirectToRoute("ManagerPage");
        }

    }
}
