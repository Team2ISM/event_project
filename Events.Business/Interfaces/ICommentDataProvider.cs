using System.Collections.Generic;
using Events.Business.Models;

namespace Events.Business.Interfaces
{
    public interface ICommentDataProvider
    {
        IList<Comment> GetAll();

        Comment GetById(string commentId);

        IList<Comment> GetByEventId(string eventId);

        IList<Comment> GetByAuthorId(string authorId);

        int Create(Comment comment);

        void Update(Comment model);

        void Delete(Comment model);
    }
}
