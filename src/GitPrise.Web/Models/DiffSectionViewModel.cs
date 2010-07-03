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
using System.IO;
using System.Linq;

namespace GitPrise.Web.Models
{
    public class DiffSectionViewModel
    {
        public List<LineViewModel> Lines { get; set; }

        private void AddLine(int? lineA, int? lineB, string text, Diff.EditType lineType)
        {
            Lines.Add(new LineViewModel(lineA, lineB, text, lineType));
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

            ForEachLine(section.TextA, (idx, line) =>
                AddLine(section.BeginA + idx, null, line, section.EditWithRespectToA));

            if (section.EditWithRespectToA != Diff.EditType.Unchanged)
            {
                ForEachLine(section.TextB, (idx, line) =>
                    AddLine(null, section.BeginB + idx, line, section.EditWithRespectToB));
            }
        }

        private static LineType GetLineType(Diff.EditType editType)
        {
            var lineType = LineType.Unchanged;
            switch (editType)
            {
                case Diff.EditType.Unchanged:
                    lineType = LineType.Unchanged;
                    break;
                case Diff.EditType.Inserted:
                    lineType = LineType.Inserted;
                    break;
                case Diff.EditType.Deleted:
                    lineType = LineType.Removed;
                    break;
                case Diff.EditType.Replaced:
                    lineType = LineType.Removed;
                    break;
            }
            return lineType;
        }
    }
}