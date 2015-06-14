using System;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Web.Mvc.Html;

namespace team2project.Helpers
{
    public static class ImageHelper
    {
        public static MvcHtmlString CreateImage(this HtmlHelper html, string url, string classAttr = "")
        {
            TagBuilder img = new TagBuilder("img");
            img.AddCssClass(classAttr);

            img.MergeAttribute("src", url);
            img.MergeAttribute("onerror", "this.src = 'http://www.tarcbusinessreview.com/wp-content/plugins/wp-voting-contest/assets/image//img_not_available.png'");

            return new MvcHtmlString(img.ToString());
        }

        public static MvcHtmlString ImageLink(this HtmlHelper html, string src, string actionName, string controllerName, object routeValues, string classAttr = "")
        {
            UrlHelper urlHelper = ((Controller)html.ViewContext.Controller).Url;
            string imgtag = html.CreateImage(src, classAttr).ToString();
            string url = urlHelper.Action(actionName, controllerName, routeValues);

            TagBuilder imglink = new TagBuilder("a");
            imglink.MergeAttribute("href", url);
            imglink.InnerHtml = imgtag;

            return new MvcHtmlString(imglink.ToString());
        }
    }
}
