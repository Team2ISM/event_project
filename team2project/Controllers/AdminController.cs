using Events.Business.Classes;
using System.Collections.Generic;
using System.Web.Mvc;
using team2project.Models;
using System.Web.Script.Serialization;
using team2project.Helpers;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using Events.Business.Helpers;

namespace team2project.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        EventManager manager;
        CommentManager commentManager;
        CitiesManager cityManager;

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

        [HttpPost]
        public ActionResult GetEvents()
        {
            List<EventViewModel> list = AutoMapper.Mapper.Map<List<EventViewModel>>(manager.GetAllEvents());

            foreach (var Event in list)
            {
                Event.Location = cityManager.GetById(Event.LocationId).Name;
            }

            new Thread(() => MarkAsSeen()).Start();

            return Json(
                        new JsonResultHelper()
                        {
                            Data = list,
                            Message = "Success: Get list of all events",
                            State = JsonResultHelper.ResponseStatus.Success
                        }
                    );
        }

        [HttpPost]
        public ActionResult Activate(string id)
        {
            EventStatuses result = manager.Activate(id);
            JsonResultHelper dataResult = null;
            switch (result)
            {
                case EventStatuses.ToggleOK:

                    dataResult = new JsonResultHelper()
                    {
                        Data = null,
                        Message = "Success: Activate Event",
                        State = JsonResultHelper.ResponseStatus.Success
                    };

                    break;
                case EventStatuses.NotExist:
                    dataResult = new JsonResultHelper()
                         {
                             Message = "Событие было удалено",
                             State = JsonResultHelper.ResponseStatus.Error
                         };
                    break;
                case EventStatuses.WasToggled:
                    dataResult = new JsonResultHelper()
                    {
                        Message = "Событие уже было активировано",
                        State = JsonResultHelper.ResponseStatus.Error
                    };
                    break;
            }
            return Json(dataResult);
        }

        [HttpPost]
        public ActionResult Deactivate(string id)
        {
            EventStatuses result = manager.Activate(id);
            JsonResultHelper dataResult = null;
            switch (result)
            {
                case EventStatuses.ToggleOK:

                    dataResult = new JsonResultHelper()
                    {
                        Data = null,
                        Message = "Success: Deactivate Event",
                        State = JsonResultHelper.ResponseStatus.Success
                    };

                    break;
                case EventStatuses.NotExist:
                    dataResult = new JsonResultHelper()
                    {
                        Message = "Событие было удалено",
                        State = JsonResultHelper.ResponseStatus.Error
                    };
                    break;
                case EventStatuses.WasToggled:
                    dataResult = new JsonResultHelper()
                    {
                        Message = "Событие уже было деактивировано",
                        State = JsonResultHelper.ResponseStatus.Error
                    };
                    break;
            }
            return Json(dataResult);
        }

        [HttpPost]
        public ActionResult DeleteEvent(string id)
        {
            if (manager.GetById(id) == null)
            {
                return Json(new JsonResultHelper()
                {
                    Data = null,
                    Message = "Failure: Event already deleted",
                    State = JsonResultHelper.ResponseStatus.Error
                });
            }
            commentManager.DeleteByEventId(id);
            manager.Delete(id);
            return Json(new JsonResultHelper()
            {
                Data = null,
                Message = "Success: Delete Event",
                State = JsonResultHelper.ResponseStatus.Success
            });
        }

        void MarkAsSeen()
        {
            var list = manager.GetAllEvents();
            foreach (var elem in list)
            {
                manager.MarkAsSeen(elem.Id);
            }
        }
    }
}
