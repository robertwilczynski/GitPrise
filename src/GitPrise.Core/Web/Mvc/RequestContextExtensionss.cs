using System;
using System.Web.Routing;

namespace GitPrise.Core.Web.Mvc
{
    public static class RequestContextExtensionss
    {
        public static string GetRepositoryName(this RequestContext @this)
        {
            return (string)@this.RouteData.DataTokens["RepositoryName"];
        }

        public static string GetTreeish(this RequestContext @this)
        {
            return (string)@this.RouteData.DataTokens["Treeish"];
        }

        public static string GetPath(this RequestContext @this)
        {
            return (string)@this.RouteData.DataTokens["Path"];
        }

        public static string GetRepositoryLocation(this RequestContext @this)
        {
            return (string)@this.RouteData.DataTokens["RepositoryLocation"];
        }

        public static void SetRepositoryName(this RequestContext @this, string value)
        {
            @this.RouteData.DataTokens.Add("RepositoryName", value);
        }

        public static void SetTreeish(this RequestContext @this, string value)
        {
            @this.RouteData.DataTokens.Add("Treeish", value);
        }

        public static void SetPath(this RequestContext @this, string value)
        {
            @this.RouteData.DataTokens.Add("Path", value);
        }

        public static void SetRepositoryLocation(this RequestContext @this, string value)
        {
            @this.RouteData.DataTokens.Add("RepositoryLocation", value);
        }

    }
}
