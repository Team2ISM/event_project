using System;

namespace Events.Business.Models
{
    public class EventModel
    {
        public virtual string Id { get; set; }

        public virtual string Title { get; set; }

        public virtual string Description { get; set; }

        public virtual string LongDescription { get; set; }

        public virtual DateTime? From { get; set; }

        public virtual DateTime? To { get; set; }

        public virtual string Location { get; set; }
    }
}