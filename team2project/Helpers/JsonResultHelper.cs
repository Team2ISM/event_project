using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace team2project.Helpers
{
    public class JsonResultHelper 
    {
        public object Data;

        public string Message;

        public StatusEnum Status { get; set; }

        public enum StatusEnum { Error = 0, Success = 1}
    }
}
