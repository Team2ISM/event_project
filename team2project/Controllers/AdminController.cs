using Events.Business.Classes;
using System.Collections.Generic;
using System.Web.Mvc;
using team2project.Models;
using System.Web.Script.Serialization;
using team2project.Helpers;

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

        [HttpPost]
        public ActionResult ToggleButtonStatusActive(string id)
        {
            manager.ToggleButtonStatusActive(id);
            return Json(
                new JsonResultHelper()
                {
                    Data = null,
                    Message = "Success: Toogle Active Event",
                    Status = JsonResultHelper.StatusEnum.Success
                }
                );
        }  

        [HttpPost]
        public ActionResult DeleteEvent(string id)
        {
            commentManager.DeleteByEventId(id);
            manager.Delete(id);
            return Json(
                new JsonResultHelper()
                {
                    Data = null,
                    Message = "Success: Toogle Active Event",
                    Status = JsonResultHelper.StatusEnum.Success
                }
                );
        }

    }
}
