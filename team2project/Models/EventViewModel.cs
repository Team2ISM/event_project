using System;

namespace team2project.Models
{
    public class EventViewModel
    {
        public uint Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public string Location { get; set; }

        public string LongDescription { get; set; }

        public EventViewModel(uint id, string title, string desc, DateTime from, DateTime to, string location = "zt", string longDesc = "")
        {
            Id = id;
            Title = title;
            Description = desc;
            From = from;
            To = to;
            Location = location;
            LongDescription = longDesc;
        }
    }
}