using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace team2project.Helpers
{
    public static class ResponseMessages
    {
        public static object NotFound = "Событие не найдено :(";
        public static object EditingNotAllowedDueToEventEndingTime = "Невозможно редактировать прошедшее событие";
    }
}