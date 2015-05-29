using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace team2project.Helpers
{
    public static class StringHelper
    {
        public static string Cut(this string data)
        {
            var result = "";
            var words = data.Split(' ');
            if (words.Length <= 10 && data.Length <= 50)
            {
                return data;
            }
            else
            {
                if (data.Length > 50)
                {
                    result = data.Substring(0, 50);
                }
                else 
                    for (var i = 0; i != 10; i++)
                    {
                        result += " " + words[i];
                    }
                result += "...";
                return result;
            }

        }
    }
}