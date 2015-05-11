using System;

namespace team2project.Models
{
    public class EventViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public string Location { get; set; }

        public string LongDescription { get; set; }

    }
}