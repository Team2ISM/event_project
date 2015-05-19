using System.Collections.Generic;
using Events.Business.Interfaces;
using Events.Business.Models;

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

        public void Create(string key, Comment model)
        {
            dataProvider.Create(model);
            cacheManager.ToCache<Comment>(key,
                () =>
                {
                    return model;
                });
            cacheManager.RemoveFromCache("commentsList");

        }

        public Comment GetById(string id)
        {
            return cacheManager.FromCache<Comment>(id,
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
    }
}
