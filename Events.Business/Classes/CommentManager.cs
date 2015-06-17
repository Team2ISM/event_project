using System.Collections.Generic;
using Events.Business.Models;
using Events.Business.Interfaces;

namespace Events.Business.Classes
{
    public class CommentManager : BaseManager
    {
        ICommentDataProvider dataProvider;

        protected override string Name { get; set; }

        public CommentManager(ICommentDataProvider dataProvider, ICacheManager cacheManager)
            : base(cacheManager)
        {
            Name = "Comments";
            this.dataProvider = dataProvider;
        }

        public IList<Comment> GetList()
        {
            return FromCache<IList<Comment>>("commentsList",
                () =>
                {
                    return dataProvider.GetAll();
                });
        }

        public void Create(Comment model)
        {
            dataProvider.Create(model);
            RemoveFromCache("commentsList");
            RemoveFromCache("commentsToEvent/" + model.EventId);
        }

        public IList<Comment> GetByEventId(string eventId)
        {
            return FromCache<IList<Comment>>("commentsToEvent/" + eventId,
                () =>
                {
                    return dataProvider.GetByEventId(eventId);
                });
        }

        public IList<Comment> GetByAuthorId(string authorId)
        {
            return FromCache<IList<Comment>>("commentsByUser/" + authorId,
                () =>
                {
                    return dataProvider.GetByAuthorId(authorId);
                });
        }

        public void DeleteByEventId(string eventId)
        {
            dataProvider.DeleteByEventId(eventId);
            RemoveFromCache("commentsList");
            RemoveFromCache("commentsToEvent/" + eventId);
        }
    }
}
