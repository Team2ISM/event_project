using Events.Business.Classes;
using Events.Business.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using team2project.Models;
using System.Web.Helpers;
using Events.NHibernateDataProvider.NHibernateCore;

namespace team2project.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Authorize(Users="team2project222@gmail.com")]
    public class AdminController : Controller
    {
        EventManager manager;

        public AdminController(EventManager manager)
        {
            this.manager = manager;
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
            manager.Delete(id);
            return RedirectToRoute("ManagerPage");
        }

    }
}
