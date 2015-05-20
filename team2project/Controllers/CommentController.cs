using System.Web.Script.Serialization;
using Events.Business.Classes;
using Events.Business.Models;
using Comments.Business.Models;
using Comments.Business.Classes;
using System.Collections.Generic;
using System.Web.Mvc;
using team2project.Models;


namespace team2project.Controllers
{
    public class CommentController : Controller
    {

        CommentManager commentManager;
        EventManager eventManager;
    
        public CommentController(EventManager eventManager, CommentManager commentManager)
        {
            this.eventManager = eventManager;
            this.commentManager = commentManager;
        }
        
        [HttpPost]
        public string GetCommentsByEventId(string id)
        {
            var evnt = AutoMapper.Mapper.Map<EventViewModel>(eventManager.GetById(id));

            if (evnt == null)
            {
                return "[]";
            }

            var comments = commentManager.GetByEventId(id);

            return new JavaScriptSerializer().Serialize(comments);
        }
        [HttpPost]
        public ActionResult AddComment(Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var commentModel = AutoMapper.Mapper.Map<Comment>(comment);
            commentManager.Create(commentModel.Id, commentModel);
            return View();
        }

    }
}
