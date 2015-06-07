using Events.Business.Classes;
using System.Collections.Generic;
using System.Web.Mvc;
using team2project.Models;
using System.Web.Script.Serialization;
using team2project.Helpers;
using System.Runtime.Remoting.Messaging;

namespace team2project.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        EventManager manager;
        CommentManager commentManager;
        CitiesManager cityManager;
        delegate bool mark();

        public AdminController(EventManager manager, CommentManager commentManager, CitiesManager cityManager)
        {
            this.manager = manager;
            this.commentManager = commentManager;
            this.cityManager = cityManager;
        }

        [HttpGet]
        public ActionResult ManagerPage()
        {

            return View();
        }

        [HttpGet]
        public ActionResult GetEvents()
        {
            List<EventViewModel> list = AutoMapper.Mapper.Map<List<EventViewModel>>(manager.GetAllEvents(true));
            foreach (var Event in list)
            {
                Event.Location = cityManager.GetById(Event.LocationId).Name;
            }
            //temporary bydlokod
            mark func = MarkAsSeen;
            func.BeginInvoke((ir) =>
            {
                var a = ((mark)(((AsyncResult)ir).AsyncDelegate)).EndInvoke(ir);
            }, null);
            //

            return Json(
                new JsonResultHelper()
                {
                    Data = list,
                    Message = "Success: Get list of all events",
                    Status = JsonResultHelper.StatusEnum.Success
                },
                JsonRequestBehavior.AllowGet
                );
        }

        [HttpPost]
        public ActionResult Activate(string id)
        {
            if (manager.Activate(id))
            {
                return Json(
                    new JsonResultHelper()
                    {
                        Data = null,
                        Message = "Success: Activate Event",
                        Status = JsonResultHelper.StatusEnum.Success
                    }
                    );
            }
            else
            {
                return Json(
                     new JsonResultHelper()
                     {
                         Message = "Failure: Event already activated",
                         Status = JsonResultHelper.StatusEnum.Error
                     }
                     );
            }
        }

        [HttpPost]
        public ActionResult Deactivate(string id)
        {
            if (manager.Deactivate(id))
            {
                return Json(
                 new JsonResultHelper()
                 {
                     Message = "Success: Deactivate Event",
                     Status = JsonResultHelper.StatusEnum.Success
                 }
                 );
            }
            else
            {
                return Json(
                     new JsonResultHelper()
                     {
                         Message = "Failure: Event already deactivated",
                         Status = JsonResultHelper.StatusEnum.Error
                     }
                     );
            }
        }

        [HttpPost]
        public ActionResult DeleteEvent(string id)
        {
            JsonResultHelper result = null;
            if (manager.GetById(id) == null)
            {
                result = new JsonResultHelper()
                {
                    Data = null,
                    Message = "Failure: Event already deleted",
                    Status = JsonResultHelper.StatusEnum.Error
                };
            }
            else
            {
                commentManager.DeleteByEventId(id);
                manager.Delete(id);
                result = new JsonResultHelper()
                {
                    Data = null,
                    Message = "Success: Delete Event",
                    Status = JsonResultHelper.StatusEnum.Success
                };
            }

            return Json(result);
        }

        bool MarkAsSeen()
        {
            var list = manager.GetAllEvents(true);
            foreach (var elem in list)
            {
                manager.MarkAsSeen(elem.Id);
            }
            return true;
        }
    }
}
