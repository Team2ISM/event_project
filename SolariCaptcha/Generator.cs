using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolariCaptcha
{
    static class Generator
    {
        public static string GetCaptcha(int number, string url)
        {
            var result = "";
            var temp = number.ToString();
            foreach(var element in temp)
            {
                result += "<img src=\"http://"+url+"/Content/Captcha/" + element + ".png\">";
            }
            return result;
        }

        public static int GetNumber(int length)
        {
            var generator = new Random();
            var result = 0;
            for (; length > 0; length--)
            {
                result *= 10;
                result += generator.Next(10);
            }
            return result;
        }
    }
}
