using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace team2project.Helpers
{
    public static class StringHelper
    {
        public static string Cut(this string data, int maxChars = 75, int maxWords = 10)
        {
            var result = "";
            if (data.Length <= maxChars) return data;
            else
            {
                data = data.Substring(0, maxChars);
                var words = data.Split(' ');
                foreach (var word in words)
                    result += " " + word;
                result += "...";   
            }
            return result;
        }

        public static string StripTags(this string htmlString)
        {
            string pattern = @"(?:<|>)";
            return Regex.Replace(htmlString, pattern, string.Empty);
        }

        public static IHtmlString ToHtmlString(this string data)
        {
            return new HtmlString(data);
        }
    }
}