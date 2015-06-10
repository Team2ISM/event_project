using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Events.Business.Helpers
{
    public static class EventStatus
    {
        public static enum EventStatuses { ToggleOK = 0, WasToggled = 1, NotExist = 2 };
    }
}