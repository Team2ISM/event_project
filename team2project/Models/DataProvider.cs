using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace team2project.Models
{
  
    public class DataProvider
    {
        private Dictionary<uint, Event> eventList;
        public void AddEvent(Event obj) {
            eventList.Add(obj.Id, obj);
        }
        public DataProvider()
        {
            this.eventList = new Dictionary<uint, Event>();
            AddEvent(new Event(1, "Cleaning before May holidays", "garbage collection on a large scale, as well as landscaping the adjacent territory",
                new DateTime(2015, 05, 4, 9, 0, 0), new DateTime(2015, 05, 4, 9, 0, 0),"Zhytomyr, gathering at the main entrance to the water park",
                "All who wish to purify our water park from garbage, or help green the water park, priglashayutsya !!! We gather at the main entrance to the water park You will be divided into 2 groups - one to pick up trash, plant trees and gardens and the other near the territorrii hydro. Please bring gloves, garbage bags, work clothes, as well as attitude to work .Orudie work and a light lunch provided.eum fugiat quo voluptas nulla pariatur"));
            AddEvent(new Event(2, "Tour History of Zhytomyr", "Tour of the historic sites of the city of Zhytomyr", new DateTime(2015, 06, 1, 13, 0, 0),
                new DateTime(2015, 06, 1, 16, 0, 0),
                "Zhitomir, gathering at the base of the monument Zhitomir, then home church of Justice, the Transfiguration Cathedral, St. Michael Street"));
            AddEvent(new Event(3, "Quest Scrabble", "the quest for those who  want to show their knowleadge or simply have fun",
                new DateTime(2015, 09, 1, 15, 0, 0), new DateTime(2015, 06, 1, 19, 0, 0),
                "Zhytomyr, gathering at the monument to Korolev S.P., then park named after Yuri Gagarin"));
           AddEvent(new Event(4, "Meeting of entomologists","Meeting of entomologists who live Zhytomyr and people who are interested in insects",
                new DateTime(2015, 09, 5, 9, 0, 0), new DateTime(2015, 09, 5, 11, 0, 0),
                "Zhitomir, gathering at the main entrance to the water park, then the forest beyond the waterpark"));
            AddEvent(new Event(5, "Quiz The Smartest","Quiz for fans of the game Most intelligent or simply educated people",
                new DateTime(2015, 10, 15, 13, 0, 0), new DateTime(2015, 06, 1, 17, 0, 0),"Zhytomyr, caffee Druz³ i Cava"));
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
