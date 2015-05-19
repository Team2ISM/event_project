using System.Collections.Generic;
using Events.Business.Models;

namespace Events.Business.Interfaces
{
    interface ICommentDataProvider
    {
        IList<Comment> GetAll();

        Comment GetById(string commentId);

        IList<Comment> GetByEventId(string eventId);

        int Create(Comment comment);
    }
}
