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
using GitPrise.Web.Models;

namespace GitPrise.Web.Mvc
{
    public static class UrlExtensions
    {
        
        public static string Script(this UrlHelper helper, string fileName)
        {
            TagBuilder builder = new TagBuilder("script");
            builder.Attributes["type"] = "text/javascript";
            builder.Attributes["src"] = helper.Content(String.Format("~/Scripts/{0}", fileName));
            return builder.ToString();
        }

        public static string Stylesheet(this UrlHelper helper, string fileName)
        {
            TagBuilder builder = new TagBuilder("link");
            builder.Attributes["rel"] = "stylesheet";
            builder.Attributes["type"] = "text/css";
            builder.Attributes["href"] = helper.Content(String.Format("~/Content/{0}", fileName));

            return builder.ToString(TagRenderMode.SelfClosing);
        }

        public static string Image(this UrlHelper helper, string fileName)
        {
            return helper.Content(String.Format("~/Content/images/{0}", fileName));
        }

        public static string LogOn(this UrlHelper helper)
        {
            return helper.Content(helper.Action("LogOn", "Account"));
        }

        public static string GetServerStatus(this UrlHelper helper)
        {
            return helper.Content(helper.Action("GetServerStatus", "Home"));
        }

        public static string GitUrl(this UrlHelper helper, string action, RepositoryNavigationRequest request)
        {
            var url = helper.Action(action, "Repository", new
            {
                repositoryName = request.RepositoryName,
                id = request.Treeish,
                path = request.Path,
                location = request.RepositoryLocation
            });
            return url;
        }

        public static string TreeUrl(this UrlHelper helper, RepositoryNavigationRequest request)
        {
            return GitUrl(helper, "tree", request);
        }

        public static string CommitUrl(this UrlHelper helper, RepositoryNavigationRequest request)
        {
            return GitUrl(helper, "commit", request);
        }

        public static string BlobUrl(this UrlHelper helper, RepositoryNavigationRequest request)
        {
            return GitUrl(helper, "blob", request);
        }

        public static string RepositoryRootUrl(this UrlHelper helper, RepositoryNavigationRequest request)
        {
            var url = helper.Action("Details", "Repository", new
            {
                repositoryName = request.RepositoryName,
                location = request.RepositoryLocation
            });
            return url;
        }
    }

}