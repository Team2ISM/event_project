using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SolariCaptcha
{
    public static class CaptchaExtension
    {
        public static IHtmlString Captcha(this HtmlHelper helper, string url, string idInput)
        {
            var length = 5;
            var idValidationMsg = Guid.NewGuid().ToString();
            var key = new Random().Next(int.MaxValue); 
            var number = Generator.GetNumber(length);
            string numbersHtmlCode = Generator.GetCaptcha(number, url);
            string textbox = "<input type=\"text\" class=\"text-box single-line\" id=\"" + idInput + "\" oninput=\" document.getElementById('" + idValidationMsg + "').innerHTML = '';\" placeholder=\"Введите цифру с картинки\">";
            string validationspan = "<span class=\"field-validation-error\" id=\""+ idValidationMsg +"\"></span>";
            string script = "<script>var confirmCaptcha = function (a) { a = a ^ " + key + "; var r = " + (number ^ Convert.ToInt32(key)).ToString() + "; if (a != r) document.getElementById('" + idValidationMsg + "').innerHTML = 'Неверно введена капча!'; return (a == r);} </script>";
            return new HtmlString(script + numbersHtmlCode + textbox + validationspan);
        }
    }
}
