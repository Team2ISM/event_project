using System;

namespace Events.Business.Models
{
    public class Comment
    {
        public virtual string Id { get; set; }

        public virtual string EventId { get; set; }

        public virtual string Text { get; set; }

        public virtual string AuthorName { get; set; }

        public virtual DateTime PostingTime { get; set; }

    }
}
