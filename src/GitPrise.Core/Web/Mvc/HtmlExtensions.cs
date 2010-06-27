#region License

// Copyright 2010 Robert Wilczynski (http://github.com/robertwilczynski/gitprise)
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace GitPrise.Core.Web.Mvc
{
    public static class HtmlExtensions
    {
        private const char PathSeparator = '/';

        public static MvcHtmlString Image(this HtmlHelper helper, string location, string alternateText, object htmlAttributes = null)
        {
            var builder = new TagBuilder("img");
            builder.Attributes.Add("src", UrlHelper.GenerateContentUrl(String.Format("~/Content/images/{0}", location), helper.ViewContext.HttpContext));
            if (!String.IsNullOrEmpty(alternateText))
            {
                builder.Attributes.Add("alt", alternateText);
            }
            if (htmlAttributes != null)
            {
                builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            }
            return MvcHtmlString.Create(builder.ToString());
        }

        public static MvcHtmlString DateTime(this HtmlHelper helper, DateTimeOffset? value)
        {
            if (!value.HasValue)
            {
                return MvcHtmlString.Empty;
            }
            var builder = new TagBuilder("span");
            builder.SetInnerText(value.Value.ToLocalTime().DateTime.ToShortDateString());
            builder.Attributes["title"] = value.Value.ToString("s");
            builder.AddCssClass("timeago");
            return MvcHtmlString.Create(builder.ToString());
        }

        public static string RepositoryRootUrl(this HtmlHelper helper)
        {
            var repositoryName = helper.ViewContext.RequestContext.GetRepositoryName();
            var repositoryLocation = helper.ViewContext.RequestContext.GetRepositoryLocation();
            UrlHelper urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            var url = urlHelper.Action("Details", "Repository", new
            {
                repositoryName = repositoryName,
                location = repositoryLocation
            });
            return url;
        }

        public static string GitUrl(this HtmlHelper helper, string action, string treeish = null, string path = null)
        {
            var repositoryName = helper.ViewContext.RequestContext.GetRepositoryName();
            var repositoryLocation = helper.ViewContext.RequestContext.GetRepositoryLocation();
            UrlHelper urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            var url = urlHelper.Action(action, "Repository", new
            {
                repositoryName = repositoryName,
                id = String.IsNullOrEmpty(treeish)
                    ? helper.ViewContext.RequestContext.GetTreeish()
                    : treeish,
                path = path,
                location = repositoryLocation
            });
            return url;
        }

        public static string TreeUrl(this HtmlHelper helper, string treeish, string path)
        {
            return GitUrl(helper, "tree", treeish, path);
        }

        public static string CommitUrl(this HtmlHelper helper, string treeish)
        {
            return GitUrl(helper, "commit", treeish, null);
        }

        public static string TreeUrl(this HtmlHelper helper, string treeish)
        {
            string path = String.Empty;
            return TreeUrl(helper, treeish, path);
        }

        public static string BlobUrl(this HtmlHelper helper, string path)
        {
            string treeish = helper.ViewContext.RequestContext.GetTreeish();
            return BlobUrl(helper, treeish, path);
        }

        public static string BlobUrl(this HtmlHelper helper, string treeish, string path)
        {
            return GitUrl(helper, "blob", treeish, path);
        }
    }
}
