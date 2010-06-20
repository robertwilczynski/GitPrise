using System;
using System.Web.Mvc;
using GitSharp;

namespace Gwit.Core.Web.Mvc
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

        public static string LogOn(this UrlHelper helper)
        {
            return helper.Content(helper.Action("LogOn", "Account"));
        }

        public static string GetServerStatus(this UrlHelper helper)
        {
            return helper.Content(helper.Action("GetServerStatus", "Home"));
        }

        public static string GitObject(this UrlHelper helper, AbstractObject @object)
        {
            string action = String.Empty;
            string name = String.Empty;

            if (@object.IsBlob)
            {
                action = "blob";
                if (@object is Leaf)
                {
                    name = (@object as Leaf).Name;
                }
            }
            else if (@object.IsTree)
            {
                action = "tree";
                name = (@object as Tree).Name;
            }
            else if (@object.IsCommit)
            {
                action = "commit";
            }
            else if (@object.IsTag)
            {
                action = "tag";
            }

            var actionUrl = helper.Action(action, "Repository", new { id = @object.Hash, name = name });

            return actionUrl;
        }
    }

}