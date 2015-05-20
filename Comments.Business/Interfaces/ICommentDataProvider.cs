using System.Collections.Generic;
using Comments.Business.Models;

namespace Comments.Business.Interfaces
{
    public interface ICommentDataProvider
    {
        IList<Comment> GetAll();

        Comment GetById(string commentId);

        IList<Comment> GetByEventId(string eventId);

        int Create(Comment comment);
    }
}
