using System.Collections.Generic;
using System.Linq;
using Events.Business.Interfaces;
using Events.Business.Models;

namespace Events.EntityFrameworkDataProvider
{
    public class EntityFrameworkCommentDataProvider : ICommentDataProvider
    {

        public IList<Comment> GetAll()
        {
            IList<Comment> list;

            using (CommentDbContext db = new CommentDbContext())
            {
                list = db.Comment.ToList<Comment>();
            }

            return list;
        }

        public Comment GetById(string id)
        {
            Comment result;

            using (CommentDbContext db = new CommentDbContext())
            {
                result = db.Comment.First(comment => comment.Id == id);
            }

            return result;
        }

        public IList<Comment> GetByEventId(string id)
        {
            IList<Comment> result;

            using (CommentDbContext db = new CommentDbContext())
            {
                result = db.Comment.Where(comment => comment.EventId == id).ToList();
            }

            return result;
        }

        public int Create(Comment model)
        {
            using (CommentDbContext db = new CommentDbContext())
            {
                db.Comment.Add(model);
                db.SaveChanges();
            }

            return 1;
        }
    }
}
