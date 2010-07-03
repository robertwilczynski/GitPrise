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

namespace GitPrise.Web.Models
{
    public class DiffViewModel
    {
        public List<LineViewModel> Lines { get; private set; }

        public DiffViewModel()
        {
            Lines = new List<LineViewModel>();
        }

        private DiffViewModel(DiffViewModel other)
            : this()
        {
            Lines.AddRange(other.Lines);
        }

        public DiffViewModel(Diff diff)
            : this()
        {
            foreach (var section in diff.Sections)
            {
                Lines.AddRange(new DiffSectionViewModel(section).Lines);
            }
        }

        /// <summary>
        /// Builds a compacted diff from this instance. Compacted diff has multiple subsequent untouched lines
        /// squashed into a single ellipsis line.
        /// </summary>
        /// <returns></returns>
        public DiffViewModel GetCompactedDiff()
        {
            var model = new DiffViewModel();
            // number of unchanged lines around the change that should be kept.
            const int unchangedWrappingLines = 3;
            var lastLineAdded = -1;
            var lines = new List<LineViewModel>();
            // number of lines left to append when the changed section is left.
            var postChangeWrapperLinesLeft = 0;
            for (int i = 0; i < Lines.Count; i++)
            {
                // change found
                var currentLine = Lines[i];
                if (currentLine.LineType != LineType.Unchanged)
                {
                    // previous line was unchanged - adding wrapper
                    if (i > 0 && Lines[i - 1].LineType == LineType.Unchanged)
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

    }
}
