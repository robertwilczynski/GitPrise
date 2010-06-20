using System;
using System.Web.Mvc;
using GitSharp;
using System.Linq;
using System.Text;
using System.Web;

namespace Gwit.Core.Web.Mvc
{
    public static class HtmlExtensions
    {
        private const char PathSeparator = '/';
        public static string Image(this HtmlHelper helper, string location, string alternateText)
        {
            var builder = new TagBuilder("img");
            builder.Attributes.Add("src", UrlHelper.GenerateContentUrl(String.Format("~/Content/images/{0}", location), helper.ViewContext.HttpContext));
            if (!String.IsNullOrEmpty(alternateText))
            {
                builder.Attributes.Add("alt", alternateText);
            }
            return builder.ToString();
        }

        public static string DateTime(this HtmlHelper helper, DateTimeOffset value)
        {
            var builder = new TagBuilder("span");
            builder.SetInnerText(value.ToLocalTime().DateTime.ToShortDateString());
            builder.Attributes["title"] = value.ToString("s");
            builder.AddCssClass("timeago");
            return builder.ToString();
        }
    }
}
