using System.Collections.Generic;
using Events.Business.Models;
using Events.Business.Interfaces;

namespace Events.Business.Classes
{
    public class CommentManager
    {
        ICommentDataProvider dataProvider;

        ICacheManager cacheManager;

        public CommentManager(ICommentDataProvider dataProvider, ICacheManager cacheManager)
        {
            this.dataProvider = dataProvider;
            this.cacheManager = cacheManager;
        }

        public IList<Comment> GetList()
        {
            return cacheManager.FromCache<IList<Comment>>("commentsList",
                () =>
                {
                    return dataProvider.GetAll();
                });
        }

        public void Create(Comment model)
        {
            dataProvider.Create(model);
            cacheManager.ToCache<Comment>("comment/" + model.Id,
                () =>
                {
                    return model;
                });
            cacheManager.RemoveFromCache("comment/" + model.Id);
            cacheManager.RemoveFromCache("commentsList");
            cacheManager.RemoveFromCache("commentsToEvent/" + model.EventId);
        }

        public Comment GetById(string id)
        {
            return cacheManager.FromCache<Comment>("comment/"+id,
                () =>
                {
                    return dataProvider.GetById(id);
                });
        }

        public IList<Comment> GetByEventId(string eventId)
        {
            return cacheManager.FromCache<IList<Comment>>("commentsToEvent/"+eventId,
                () =>
                {
                    return dataProvider.GetByEventId(eventId);
                });
        }

        public IList<Comment> GetByAuthorId(string authorId)
        {
            return cacheManager.FromCache<IList<Comment>>("commentsByUser/" + authorId,
                () =>
                {
                    return dataProvider.GetByAuthorId(authorId);
                });
        }
    }
}
