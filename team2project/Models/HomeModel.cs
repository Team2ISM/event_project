using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace team2project.Models
{
    public class Event
    {
        public uint id { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public DateTime from { get; set; }
        public DateTime to { get; set; }
        public string location { get; set; }
        public string longDesc { get; set; }
        public Event(uint id, string title, string desc, DateTime from, DateTime to, string location="zt", string longDesc = "")
        {
            this.id = id;
            this.title = title;
            this.desc = desc;
            this.from = from;
            this.to = to;
            this.location = location;
            this.longDesc = longDesc;
        }
    }
    public class HomeModel
    {
        private Dictionary<uint, Event> eventList;
        public void AddEvent(Event obj) {
            eventList.Add(obj.id, obj);
        }
        public HomeModel()
        {
            this.eventList = new Dictionary<uint, Event>();
            AddEvent(new Event(1, "title1", "desc1", new DateTime(2015, 06, 1, 12, 0, 0), new DateTime(2015, 06, 1, 13, 0, 0), "zt", "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur? Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur?"));
            AddEvent(new Event(2, "title2", "desc2", new DateTime(2015, 06, 1, 13, 0, 0), new DateTime(2015, 06, 1, 14, 0, 0)));
            AddEvent(new Event(3, "title3", "desc3", new DateTime(2015, 06, 1, 14, 0, 0), new DateTime(2015, 06, 1, 15, 0, 0), "zt", "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur? Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur?"));
            AddEvent(new Event(4, "title4", "desc4", new DateTime(2015, 06, 1, 15, 0, 0), new DateTime(2015, 06, 1, 16, 0, 0)));
            AddEvent(new Event(5, "title5", "desc5", new DateTime(2015, 06, 1, 16, 0, 0), new DateTime(2015, 06, 1, 17, 0, 0)));
            AddEvent(new Event(6, "title6", "desc6", new DateTime(2015, 06, 2, 12, 0, 0), new DateTime(2015, 06, 2, 13, 0, 0)));
            AddEvent(new Event(7, "title7", "desc7", new DateTime(2015, 06, 2, 13, 0, 0), new DateTime(2015, 06, 2, 15, 0, 0)));
            AddEvent(new Event(8, "title8", "desc8", new DateTime(2015, 06, 2, 15, 0, 0), new DateTime(2015, 06, 2, 16, 0, 0)));
            AddEvent(new Event(9, "title9", "desc9", new DateTime(2015, 06, 3, 16, 0, 0), new DateTime(2015, 06, 3, 17, 30, 0)));
            AddEvent(new Event(10, "title10", "desc10", new DateTime(2015, 06, 3, 18, 0, 0), new DateTime(2015, 06, 3, 19, 0, 0)));
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
