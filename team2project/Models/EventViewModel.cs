using System;

namespace team2project.Models
{
    public class EventViewModel
    {
        public EventViewModel() 
        {
            Id = Guid.NewGuid().ToString();
            From = DateTime.Now.AddDays(1);
            To = DateTime.Now.AddDays(1);
        }
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public string Location { get; set; }

        public string LongDescription { get; set; }

    }
}