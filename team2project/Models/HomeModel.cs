using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace team2project.Models
{
    public class Event
    {
        public uint id;
        public string title;
        public string desc;
        public DateTime from;
        public DateTime to;
        public string location;
        public Event(uint id, string title, string desc, DateTime from, DateTime to, string location)
        {
            this.id = id;
            this.title = title;
            this.desc = desc;
            this.from = from;
            this.to = to;
            this.location = location;
        }
    }
    public class HomeModel
    {
        private Dictionary<uint, Event> eventList;
        public HomeModel()
        {
            this.eventList = new Dictionary<uint, Event>();
            eventList.Add(1, new Event(1, "title1", "desc1", new DateTime(2015, 06, 1, 12, 0, 0), new DateTime(2015, 06, 1, 13, 0, 0), "zt"));
            eventList.Add(2, new Event(1, "title2", "desc2", new DateTime(2015, 06, 1, 13, 0, 0), new DateTime(2015, 06, 1, 14, 0, 0), "zt"));
            eventList.Add(3, new Event(1, "title3", "desc3", new DateTime(2015, 06, 1, 14, 0, 0), new DateTime(2015, 06, 1, 15, 0, 0), "zt"));
            eventList.Add(4, new Event(1, "title4", "desc4", new DateTime(2015, 06, 1, 15, 0, 0), new DateTime(2015, 06, 1, 16, 0, 0), "zt"));
            eventList.Add(5, new Event(1, "title5", "desc5", new DateTime(2015, 06, 1, 16, 0, 0), new DateTime(2015, 06, 1, 17, 0, 0), "zt"));
            eventList.Add(6, new Event(1, "title6", "desc6", new DateTime(2015, 06, 2, 12, 0, 0), new DateTime(2015, 06, 1, 13, 0, 0), "zt"));
            eventList.Add(7, new Event(1, "title7", "desc7", new DateTime(2015, 06, 2, 13, 0, 0), new DateTime(2015, 06, 1, 15, 0, 0), "zt"));
            eventList.Add(8, new Event(1, "title8", "desc8", new DateTime(2015, 06, 2, 15, 0, 0), new DateTime(2015, 06, 1, 16, 0, 0), "zt"));
            eventList.Add(9, new Event(1, "title9", "desc9", new DateTime(2015, 06, 3, 16, 0, 0), new DateTime(2015, 06, 1, 17, 30, 0), "zt"));
            eventList.Add(10, new Event(1, "title10", "desc10", new DateTime(2015, 06, 3, 18, 0, 0), new DateTime(2015, 06, 1, 19, 0, 0), "zt"));
        }

        public Dictionary<uint, Event> GetEvents()
        {
            return this.eventList;
        }

        public Event GetById(uint id)
        {
            return this.eventList[id];
        }
    }
}