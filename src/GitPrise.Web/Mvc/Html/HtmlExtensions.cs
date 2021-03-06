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
using GitSharp;
using System.Web.Routing;

namespace GitPrise.Web.Mvc.Html
{
    public static class HtmlExtensions
    {

        public static string DiffLine(this HtmlHelper helper, UnifiedDiffViewModel.Line line)
        {
            var builder = new TagBuilder("pre");

            string mnemonic = null;
            string cssClass = null;

            switch (line.LineType)
            {
                case Diff.EditType.Unchanged:
                    mnemonic = "&nbsp;";
                    break;
                case Diff.EditType.Inserted:
                    mnemonic = "+";
                    cssClass = "di";
                    break;
                case Diff.EditType.Deleted:
                    mnemonic = "-";
                    cssClass = "dd";
                    break;
                case Diff.EditType.Replaced:
                    if (line.LineA.HasValue)
                    {
                        mnemonic = "-";
                        cssClass = "dd";
                    }
                    else if (line.LineB.HasValue)
                    {
                        mnemonic = "+";
                        cssClass = "di";
                    }
                    break;
            }

            

            if (cssClass != null)
            {
                builder.AddCssClass(cssClass);
            }

            var innerHtml = helper.Encode(line.Text);
            if (line.LineType == Diff.EditType.Replaced)
            {
                innerHtml = String.Format("<span>{0}</span>", innerHtml);
            }

            builder.InnerHtml = mnemonic + innerHtml;

            return builder.ToString();
        }

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
    }
}