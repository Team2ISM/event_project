using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace team2project.Models
{
    public class Event
    {
        public uint Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Location { get; set; }
        public string LongDesc { get; set; }
        public Event(uint id, string title, string desc, DateTime from, DateTime to, string location = "zt", string longDesc = "")
        {
            Id = id;
            Title = title;
            Desc = desc;
            From = from;
            To = to;
            Location = location;
            LongDesc = longDesc;
        }
    }
}