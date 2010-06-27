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

namespace GitPrise.Web.Mvc.Html
{
    public static class HtmlExtensions
    {

        public static string DiffLine(this HtmlHelper helper, LineViewModel line)
        {
            var builder = new TagBuilder("pre");
            
            string mnemonic = null;
            string cssClass = null;

            switch (line.EditType)
            {
                case Diff.EditType.Unchanged:
                    mnemonic = "&nbsp;";
                    break;
                case Diff.EditType.Inserted:
                    mnemonic =  "+";
                    cssClass = "di";
                    break;
                case Diff.EditType.Deleted:
                    mnemonic =  "-";
                    cssClass = "dd";
                    break;
                case Diff.EditType.Replaced:
                    mnemonic =  "-";
                    cssClass = "dd";
                    break;
            }
            if (cssClass != null)
            {
                builder.AddCssClass(cssClass);
            }
            builder.InnerHtml = mnemonic + helper.Encode(line.Text);

            return builder.ToString();
        }
    }
}