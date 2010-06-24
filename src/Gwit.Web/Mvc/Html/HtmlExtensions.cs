using System;
using System.Web.Mvc;
using Gwit.Web.Models;
using GitSharp;

namespace Gwit.Web.Mvc.Html
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