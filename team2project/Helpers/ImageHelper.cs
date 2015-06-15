using System;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Web.Mvc.Html;

namespace team2project.Helpers
{
    public static class ImageHelper
    {
        private const string notAvailUrl = "http://www.tarcbusinessreview.com/wp-content/plugins/wp-voting-contest/assets/image//img_not_available.png";

        public static MvcHtmlString CreateImage(this HtmlHelper html, string url, string classAttr = "")
        {
            TagBuilder img = new TagBuilder("img");
            img.AddCssClass(classAttr);
            if (string.IsNullOrEmpty(url))
            {
                img.MergeAttribute("src", notAvailUrl);
            }
            else
            {
                img.MergeAttribute("src", url);
                img.MergeAttribute("onerror", "this.src = '" + notAvailUrl + "'");
            }

            return new MvcHtmlString(img.ToString());
        }

        public static MvcHtmlString ImageLink(this HtmlHelper html, string src, string actionName, string controllerName, object routeValues, string classAttr = "")
        {
            UrlHelper urlHelper = ((Controller)html.ViewContext.Controller).Url;
            string url = urlHelper.Action(actionName, controllerName, routeValues);         

            TagBuilder img = new TagBuilder("img");
            img.AddCssClass(classAttr);
            if (string.IsNullOrEmpty(src))
            {
                img.MergeAttribute("src", notAvailUrl);
            }
            else
            {
                img.MergeAttribute("src", src);
                img.MergeAttribute("onerror", "this.src = '" + notAvailUrl + "'");
            }

            TagBuilder imglink = new TagBuilder("a");
            imglink.MergeAttribute("href", url);
            imglink.AddCssClass("zoomEnabled");
            imglink.InnerHtml = img.ToString();

            return new MvcHtmlString(imglink.ToString());
        }
    }
}
