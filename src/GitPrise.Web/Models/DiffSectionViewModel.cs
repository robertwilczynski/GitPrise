using System;
using System.Collections.Generic;
using GitSharp;
using System.IO;
using System.Linq;

namespace GitPrise.Web.Models
{
    public class LineViewModel
    {
        public int? LineA { get; set; }
        public int? LineB { get; set; }
        public string Text { get; set; }
        public Diff.EditType EditType { get; set; }

        public LineViewModel(int? lineA, int? lineB, string text, Diff.EditType editType)
        {
            EditType = editType;
            LineA = lineA;
            LineB = lineB;
            Text = text;
        }
    }

    public class DiffSectionViewModel
    {
        public List<LineViewModel> Lines { get; set; }

        private void AddLine(int? lineA, int? lineB, string text, Diff.EditType editType)
        {
            Lines.Add(new LineViewModel(lineA, lineB, text, editType));
        }

        private void ForEachLine(string text, Action<int, string> action)
        {
            using (var reader = new StringReader(text))
            {
                int lineNumber = 0;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    action(lineNumber, line);
                    lineNumber += 1;
                }
            }
        }

        public DiffSectionViewModel(Diff.Section section)
        {
            Lines = new List<LineViewModel>();

            if (section.EditWithRespectToA == Diff.EditType.Unchanged)
            {
                ForEachLine(section.TextA, (idx, line) =>
                    AddLine(section.BeginA + idx, section.BeginB + idx, line, section.EditWithRespectToA));
            }
            else
            {
                //var change = section.EditWithRespectToB;
                ForEachLine(section.TextA, (idx, line) =>
                    AddLine(section.BeginA + idx, null, line, section.EditWithRespectToB));

                var change = section.EditWithRespectToA;
                if (change == Diff.EditType.Replaced)
                {
                    change = Diff.EditType.Inserted;
                }
                ForEachLine(section.TextB, (idx, line) =>
                    AddLine(null, section.BeginB + idx, line, change));
            }

            Lines.OrderBy(x => x.LineA);
        }
    }
}