using System.Collections.Generic;
using Comments.Business.Interfaces;
using Comments.Business.Models;
using Events.Business.Interfaces;

namespace Comments.Business.Classes
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
            // remove comments to flkdsjgirdshgjjsduoghwsrdhgbsjhfdgjkrdshkuyea
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
