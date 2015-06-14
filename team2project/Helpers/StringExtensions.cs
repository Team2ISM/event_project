using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace team2project.Helpers
{
    public static class StringExtensions
    {
        public static string Cut(this string data, int maxChars = 75)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data"); //Empty string is valid input
            }
            var result = "";
            if (data.Length <= maxChars)
            {
                return data;
            }
            result =  data.Substring(0, maxChars) + "...";   
            return result;
        }

        public static string RemovePreTag(this string data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data"); //Empty string is valid input
            } 
            data = data.Replace("<pre>", "");
            data = data.Replace("</pre>", "");
            return data;
        }

        public static IHtmlString ToHtmlString(this string data)
        {
            return new HtmlString(data);
        }
    }
}