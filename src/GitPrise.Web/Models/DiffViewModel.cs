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
                Lines.Add(new LineViewModel(0, 0, 
                    String.Format("{0} A => {1} {2} {3} B => {4} {5} {6}", 
                        section.Status, 
                        section.BeginA, section.EndA, section.EditWithRespectToA, 
                        section.BeginB, section.EndB, section.EditWithRespectToB),
                    LineType.Unchanged));
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
            var model = new DiffViewModel(this);
            //model.Lines
            return model;
        }

    }
}
