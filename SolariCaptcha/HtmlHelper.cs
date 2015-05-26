using System;
using System.Security.Cryptography;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SolariCaptcha
{
    public static class CaptchaExtension
    {
        public static IHtmlString Captcha(this HtmlHelper helper, int length, string url)
        {
            var idInput = Guid.NewGuid().ToString();
            var idCheckBox = Guid.NewGuid().ToString();
            var idValidationMsg = Guid.NewGuid().ToString();
            var functionName = new string(Guid.NewGuid().ToString().ToCharArray().Where(n => char.IsLetter(n)).ToArray()); 
            var key = new Random().Next(int.MaxValue); 
            int number = Generator.GetNumber(length);
            string numbersHtmlCode = Generator.GetCaptcha(number, url);
            string newLine = "<br>";
            string textbox = "<input type=\"text\" id=\"" + idInput + "\" oninput=\" document.getElementById('" + idValidationMsg + "').innerHTML = ''; " + functionName + "(this.value)\" required>";
            string checkboxhidden = "<input type=\"checkbox\" id=\"" + idCheckBox + "\" style=\"display: none\" required>";
            string validationspan = "<span class=\"field-validation-error\" id=\""+ idValidationMsg +"\"></span>";
            string script = "<script type=\"text/javascript\">var " + functionName + " = function (a) { a = a ^ " + key + "; var r = " + (number ^ Convert.ToInt32(key)).ToString() + "; document.getElementById('" + idCheckBox + "').checked = (a == r); if (a != r) document.getElementById('" + idValidationMsg + "').innerHTML = 'Неверно введена капча!';} </script>";
            return new HtmlString(script + numbersHtmlCode + newLine + textbox + checkboxhidden + newLine + validationspan);
        }

        public static IHtmlString Captcha(this HtmlHelper helper, string url)
        {
            var length = new Random().Next(4, 7);
            var idInput = Guid.NewGuid().ToString();
            var idCheckBox = Guid.NewGuid().ToString();
            var idValidationMsg = Guid.NewGuid().ToString();
            var functionName = new string(Guid.NewGuid().ToString().ToCharArray().Where(n => char.IsLetter(n)).ToArray()); 
            var key = new Random().Next(int.MaxValue); 
            int number = Generator.GetNumber(length);
            string numbersHtmlCode = Generator.GetCaptcha(number, url);
            string newLine = "<br>";
            string textbox = "<input type=\"text\" id=\"" + idInput + "\" oninput=\" document.getElementById('" + idValidationMsg + "').innerHTML = ''; " + functionName + "(this.value)\" placeholder=\"Введите цифру с картинки\" required>";
            string checkboxhidden = "<input type=\"checkbox\" id=\"" + idCheckBox + "\" style=\"display: none\" required>";
            string validationspan = "<span class=\"field-validation-error\" id=\""+ idValidationMsg +"\"></span>";
            string script = "<script type=\"text/javascript\">var " + functionName + " = function (a) { a = a ^ " + key + "; var r = " + (number ^ Convert.ToInt32(key)).ToString() + "; document.getElementById('" + idCheckBox + "').checked = (a == r); if (a != r) document.getElementById('" + idValidationMsg + "').innerHTML = 'Неверно введена капча!';} </script>";
            return new HtmlString(script + numbersHtmlCode + newLine + textbox + checkboxhidden + newLine + validationspan);
        }
    }
}
