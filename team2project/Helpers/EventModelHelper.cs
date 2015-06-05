using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using team2project.Models;
using Events.Business.Models;
namespace team2project.Helpers
{
    public static class EventModelHelper
    {
        public static EventViewModel Merge(this EventViewModel destination, Event from)
        {
            if (destination.AuthorId == null) destination.AuthorId = from.AuthorId;
            if (destination.DateOfCreation == null) destination.DateOfCreation = from.DateOfCreation;
            destination.Checked = false;
            return destination;
        }
    }
}