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
        UserManager userManager;

        public CommentController(EventManager eventManager, CommentManager commentManager, UserManager userManager)
        {
            this.eventManager = eventManager;
            this.commentManager = commentManager;
            this.userManager = userManager;
        }

        [HttpPost]
        public ActionResult AddComment(CommentViewModel commentModel)
        {
            if (commentModel.EventId == null)
            {
                return RedirectToRoute("eventDetails");
            }
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var user = userManager.GetByEmail(User.Identity.Name);
                commentModel.AuthorId = user.Id;
                commentModel.AuthorName = user.Name + " " + user.Surname;
            }
            var comment = AutoMapper.Mapper.Map<Comment>(commentModel);
            comment.PostingTime = DateTime.Now;
            commentManager.Create(comment);
            return RedirectToRoute("eventDetails", new { id = commentModel.EventId });
        }

    }
}
