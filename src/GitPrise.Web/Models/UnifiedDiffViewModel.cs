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
using System.Collections.Generic;
using GitSharp;
using System.Linq;
using System.IO;

namespace GitPrise.Web.Models
{
    public class UnifiedDiffViewModel
    {
        public List<Line> Lines { get; private set; }

        public UnifiedDiffViewModel()
        {
            Lines = new List<Line>();
        }

        private UnifiedDiffViewModel(UnifiedDiffViewModel other)
            : this()
        {
            Lines.AddRange(other.Lines);
        }

        public UnifiedDiffViewModel(Diff diff)
            : this()
        {
            foreach (var section in diff.Sections)
            {
                Lines.AddRange(GetLinesFromSection(section));
            }
        }

        /// <summary>
        /// Builds a compacted diff from this instance. Compacted diff has multiple subsequent untouched lines
        /// squashed into a single ellipsis line.
        /// </summary>
        /// <returns></returns>
        public UnifiedDiffViewModel GetCompactedDiff()
        {
            var model = new UnifiedDiffViewModel();
            // number of unchanged lines around the change that should be kept.
            const int unchangedWrappingLines = 3;
            var lastLineAdded = -1;
            var lines = new List<Line>();
            // number of lines left to append when the changed section is left.
            var postChangeWrapperLinesLeft = 0;
            for (int i = 0; i < Lines.Count; i++)
            {
                // change found
                var currentLine = Lines[i];
                if (currentLine.LineType != Diff.EditType.Unchanged)
                {
                    // previous line was unchanged - adding wrapper
                    if (i > 0 && Lines[i - 1].LineType == Diff.EditType.Unchanged)
                    {
                        // taking unchangedWrappingLines before first changed line
                        var firstWrappingLine = Math.Max(0, i - unchangedWrappingLines);
                        // making sure we don't grab a line we already added
                        firstWrappingLine = Math.Max(lastLineAdded + 1, firstWrappingLine);
                        for (int wrapperIndex = firstWrappingLine; wrapperIndex < i; wrapperIndex++)
                        {
                            lines.Add(Lines[wrapperIndex]);
                        }
                    }
                    lines.Add(currentLine);
                    lastLineAdded = i;
                    // how many unchanged lines we want to append after we append all changed lines.
                    postChangeWrapperLinesLeft = unchangedWrappingLines;
                }
                else
                {
                    if (postChangeWrapperLinesLeft > 0)
                    {
                        lines.Add(currentLine);
                        postChangeWrapperLinesLeft -= 1;
                        lastLineAdded = i;
                    }
                }
            }

            model.Lines.AddRange(lines);
            return model;
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
        
        private List<Line> GetLinesFromSection(Diff.Section section)
        {
            var lines = new List<Line>();

            ForEachLine(section.TextA, (idx, line) =>
                lines.Add(new Line(section.BeginA + idx, null, line, section.EditWithRespectToA)));

            if (section.EditWithRespectToA != Diff.EditType.Unchanged)
            {
                ForEachLine(section.TextB, (idx, line) =>
                    lines.Add(new Line(null, section.BeginB + idx, line, section.EditWithRespectToB)));
            }

            return lines;
        }

        public class Line
        {
            public int? LineA { get; set; }
            public int? LineB { get; set; }
            public string Text { get; set; }
            public Diff.EditType LineType { get; set; }

            public Line(int? lineA, int? lineB, string text, Diff.EditType lineType)
            {
                LineType = lineType;
                LineA = lineA;
                LineB = lineB;
                Text = text;
            }
        }
    }
}
