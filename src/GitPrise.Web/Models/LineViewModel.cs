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
using GitSharp;

namespace GitPrise.Web.Models
{
    public class LineViewModel
    {
        public int? LineA { get; set; }
        public int? LineB { get; set; }
        public string Text { get; set; }
        public Diff.EditType LineType { get; set; }

        public LineViewModel(int? lineA, int? lineB, string text, Diff.EditType lineType)
        {
            LineType = lineType;
            LineA = lineA;
            LineB = lineB;
            Text = text;
        }
    }
}
