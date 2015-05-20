using Events.Business.Classes;
using Events.Business.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using team2project.Models;
using System.Web.Helpers;

namespace team2project.Controllers
{
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
            List<EventViewModel> list = AutoMapper.Mapper.Map<List<EventViewModel>>(manager.GetList());
            return View("ManagerPage", list);
        }

        public ActionResult ToggleButtonStatusActive(string id)
        {
            manager.ToggleButtonStatusActive(id);
            return RedirectToRoute("ManagerPage");
        }

        public ActionResult ToggleButtonStatusChecked(string id)
        {
            manager.ToggleButtonStatusChecked(id);
            return RedirectToRoute("ManagerPage");
        }

    }
}
