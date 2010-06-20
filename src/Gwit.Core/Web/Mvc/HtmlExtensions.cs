using System;
using System.Web.Mvc;

namespace Gwit.Core.Web.Mvc
{
    public static class HtmlExtensions
    {
        private const char PathSeparator = '/';
        
        public static MvcHtmlString Image(this HtmlHelper helper, string location, string alternateText)
        {
            var builder = new TagBuilder("img");
            builder.Attributes.Add("src", UrlHelper.GenerateContentUrl(String.Format("~/Content/images/{0}", location), helper.ViewContext.HttpContext));
            if (!String.IsNullOrEmpty(alternateText))
            {
                builder.Attributes.Add("alt", alternateText);
            }
            return MvcHtmlString.Create(builder.ToString());
        }

        public static MvcHtmlString DateTime(this HtmlHelper helper, DateTimeOffset value)
        {
            var builder = new TagBuilder("span");
            builder.SetInnerText(value.ToLocalTime().DateTime.ToShortDateString());
            builder.Attributes["title"] = value.ToString("s");
            builder.AddCssClass("timeago");
            return MvcHtmlString.Create(builder.ToString());
        }

        public static string GitUrl(this HtmlHelper helper, string action, string treeish, string path)
        {
            var repositoryName = helper.ViewContext.RequestContext.RouteData.DataTokens["RepositoryName"] as string;
            var repositoryLocation = helper.ViewContext.RequestContext.RouteData.DataTokens["RepositoryLocation"] as string;

            UrlHelper urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            var url = urlHelper.Action(action, "Repository", new 
            {
                repositoryName = repositoryName, 
                id = treeish,
                path = path,
                location = repositoryLocation
            });
            return url;
        }

        public static string TreeUrl(this HtmlHelper helper, string treeish, string path)
        {
            return GitUrl(helper, "tree", treeish, path);
        }

        public static string TreeUrl(this HtmlHelper helper, string treeish)
        {
            string path = String.Empty;
            return TreeUrl(helper, treeish, path);
        }

        public static string BlobUrl(this HtmlHelper helper, string treeish, string path)
        {
            return GitUrl(helper, "tree", treeish, path);
        }
    }
}
