using System.Web.Script.Serialization;
using Events.Business.Classes;
using Events.Business.Models;
using Comments.Business.Models;
using Comments.Business.Classes;
using System.Collections.Generic;
using System.Web.Mvc;
using team2project.Models;
using System;


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
        public ActionResult AddComment(CommentViewModel commentModel)
        {
            if (!ModelState.IsValid)
            {
                if (commentModel.EventId == null)
                {
                    return RedirectToRoute("eventDetails");
                }
                return RedirectToRoute("eventDetails", new { @id = commentModel.EventId });
            }
            var comment = AutoMapper.Mapper.Map<Comment>(commentModel);
            comment.PostingTime = DateTime.Now;
            commentManager.Create(commentModel.Id, comment);
            return RedirectToRoute("eventDetails", new { @id=comment.EventId });
        }

    }
}
