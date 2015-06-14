using System;
using System.Web;
using System.Web.Mvc;
using System.Linq;

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
    }
}
