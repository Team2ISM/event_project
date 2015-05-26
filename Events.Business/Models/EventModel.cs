using System;

namespace Events.Business.Models
{
    public class Event
    {
        public virtual string Id { get; set; }

        public virtual string Title { get; set; }

        public virtual string Description { get; set; }

        public virtual DateTime? FromDate { get; set; }

        public virtual DateTime? ToDate { get; set; }

        public virtual string Location { get; set; }

        public virtual bool Active { get; set; }

        public virtual bool Checked { get; set; }

        public virtual DateTime? DateOfCreation { get; set; }

        public virtual string AuthorId { get; set; }
    }
}