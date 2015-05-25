using System.Web.Script.Serialization;
using Events.Business.Classes;
using Events.Business.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using team2project.Models;
using System;

using Events.NHibernateDataProvider.NHibernateCore;
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
            if (!String.IsNullOrEmpty(User.Identity.Name)) {
                var user = new NHibernateUserDataProvider().GetByMail(User.Identity.Name);
                commentModel.AuthorId = user.Id;
                commentModel.AuthorName = user.Name + " " + user.Surname;
            }
            else if (!ModelState.IsValid)
            {
                if (commentModel.EventId == null)
                {
                    return RedirectToRoute("eventDetails");
                }
                return RedirectToRoute("eventDetails", new { @id = commentModel.EventId });
            }
            var comment = AutoMapper.Mapper.Map<Comment>(commentModel);
            comment.PostingTime = DateTime.Now;
            commentManager.Create(comment);
            return RedirectToRoute("eventDetails", new { @id=comment.EventId });
        }

    }
}
