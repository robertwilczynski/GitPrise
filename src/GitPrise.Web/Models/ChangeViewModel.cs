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
using System.Linq;

namespace GitPrise.Web.Models
{
    public class ChangeViewModel : RepositoryNavigationRequest
    {
        public Change Change { get; private set; }
        public UnifiedDiffViewModel Diff { get; private set; }
        public string Name { get; private set; }
        public SummaryViewModel Summary { get; private set; }


        public ChangeViewModel(RepositoryNavigationRequest request, Change change, Diff diff)
            : base(request)
        {
            Change = change;
            Treeish = change.ComparedCommit.Hash;
            Name = System.IO.Path.GetFileName(change.Path);
            Path = change.Path;
            Diff = new UnifiedDiffViewModel(diff);
            Summary = new SummaryViewModel();
            Summary.Inserts = Diff.Lines.Count(x => 
                x.LineType == GitSharp.Diff.EditType.Inserted || 
                (x.LineType == GitSharp.Diff.EditType.Replaced && x.LineB.HasValue) // new ones
            );

            Summary.Deletes = Diff.Lines.Count(x => 
                x.LineType == GitSharp.Diff.EditType.Deleted || 
                (x.LineType == GitSharp.Diff.EditType.Replaced && x.LineA.HasValue) // replaced with new one(s)
            );
        }

        public class SummaryViewModel
        {
            public int Inserts { get; internal set; }
            public int Deletes { get; internal set; }
            public int Changes { get { return Inserts + Deletes; } }
        }

    }
}
