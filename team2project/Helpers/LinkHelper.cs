using System;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Web.Mvc.Html;

namespace team2project.Helpers
{
    public static class LinkHelper
    {
        public static HtmlString CreateLink(this HtmlHelper html, string innerHtml, string routeName, object routeValues, object htmlAttributes)
        {
            var a = html.RouteLink(innerHtml, routeName, routeValues, htmlAttributes);
            return MvcHtmlString.Create(HttpUtility.HtmlDecode(a.ToHtmlString()));            
        }
    }
}