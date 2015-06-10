using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Events.Business.Models;
using Events.Business.Interfaces;

namespace Events.NHibernateDataProvider.NHibernateCore
{
    public class NHibernateCommentDataProvider : ICommentDataProvider
    {
        public IList<Comment> GetAll()
        {
            using (ISession session = Helper.OpenSession())
            {
                var criteria = session.CreateCriteria<Comment>();
                criteria.AddOrder(Order.Desc("PostingTime"));
                return criteria.List<Comment>();
            }
        }

        public Comment GetById(string id)
        {
            Comment Model;
            using (ISession session = Helper.OpenSession())
            {
                Model = session.Get<Comment>(id);
            }
            return Model;
        }

        public IList<Comment> GetByEventId(string eventId)
        {
            using (ISession session = Helper.OpenSession())
            {
                var criteria = session.CreateCriteria<Comment>();
                criteria.Add(Expression.Eq("EventId", eventId));
                criteria.AddOrder(Order.Desc("PostingTime"));
                return criteria.List<Comment>();
            }
        }

        public IList<Comment> GetByAuthorId(string authorId)
        {
            using (ISession session = Helper.OpenSession())
            {
                var criteria = session.CreateCriteria<Comment>();
                criteria.Add(Expression.Eq("AuthorId", authorId));
                criteria.AddOrder(Order.Desc("PostingTime"));
                return criteria.List<Comment>();
            }
        }

        public void Create(Comment model)
        {        
            using (ISession session = Helper.OpenSession())
            {
                    session.Save(model);            
            }
        }

        public void Update(Comment model)
        {
            using (ISession session = Helper.OpenSession())
            {
                session.Update(model);             
            }
        }

        public void Delete(Comment model)
        {
            using (ISession session = Helper.OpenSession())
            {
                session.Delete(model);             
            }
        }

        public void DeleteByEventId(string id)
        {
            using (ISession session = Helper.OpenSession())
            {
                using (ITransaction tran = session.BeginTransaction())
                {
                    var criteria = session.CreateCriteria<Comment>();
                    criteria.Add(Expression.Eq("EventId", id));
                    var list = criteria.List<Comment>();
                    foreach(var comment in list)
                    {
                        session.Delete(comment);
                    }
                    tran.Commit();
                }
            }
        }
    }
}
