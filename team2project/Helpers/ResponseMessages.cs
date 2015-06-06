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
        public static object EditingNotAllowedDueToWrongUser = "Редактировать событие может только его создатель";
        public static object DeletingNotAllowedDueToWrongUser = "Удалить событие может только его создатель";
        public static object AccesDenied = "Недостаточный уровень доступа. Доступ закрыт";
    }
}