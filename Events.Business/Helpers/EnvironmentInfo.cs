using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Business.Helpers
{
    public static class EnvironmentInfo
    {
        public static string Host { get; set; }
        public static string Email
        {
            get
            {
                return "team2project222@gmail.com";
            }
            private set
            {
            }
        }
    }
}
