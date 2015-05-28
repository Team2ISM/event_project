using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace SolariCaptcha
{
    static class Generator
    {
        public static string GetCaptcha(int number, string url)
        {
            var rand = new Random();
            var crypto = new MD5CryptoServiceProvider();
            var result = "<p style=\"background: url(http://" + url + "/Content/Captcha/bg.png) left repeat-x; display: inline-block;\">";         
            var temp = number.ToString();
            foreach(var element in temp)
            {
                var picName = BitConverter.ToInt32(crypto.ComputeHash(BitConverter.GetBytes(Convert.ToInt32(element))), 0);
                var rotation = rand.Next(-30, 30);
                result += "<img src=\"http://" + url + "/Content/Captcha/" + picName + ".png\" style=\"transform: rotate(" + rotation.ToString() + "deg)\">";
            }
            result += "</p>";
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
