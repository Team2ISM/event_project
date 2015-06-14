using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace team2project
{
    class FirstRequestInitialisation
    {
        private static string host = null;

        private static Object s_lock = new Object();

        public static string Initialise(HttpContext context)
        {
            if (string.IsNullOrEmpty(host))
            {
                lock (s_lock)
                {
                    if (string.IsNullOrEmpty(host))
                    {
                        Uri uri = HttpContext.Current.Request.Url;
                        host = uri.Scheme + Uri.SchemeDelimiter + uri.Host + ":" + uri.Port;
                    }
                }
            }

            return host;
        }
    }
}