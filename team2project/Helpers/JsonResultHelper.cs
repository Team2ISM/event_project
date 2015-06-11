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

        public ResponseStatus State { get; set; }

        public enum ResponseStatus
        {
            Error,
            Success
        }
    }
}
