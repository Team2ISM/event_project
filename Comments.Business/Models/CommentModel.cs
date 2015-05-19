using System;

namespace Events.Business.Models
{
    class Comment
    {
        public virtual string Id { get; set; }

        public virtual string EventID { get; set; }

        public virtual string Text { get; set; }

        public virtual string AutorName { get; set; }

        public virtual DateTime PostingTime { get; set; }

    }
}
