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
using System.Collections.Generic;
using System.Linq;

namespace GitPrise.Web.Models
{
    public class CommitViewModel : RepositoryNavigationRequest
    {
        public List<CommitViewModel> Parents { get; set; }
        public TreeNodeViewModel Tree { get; set; }
        public Commit Commit { get; set; }

        public CommitViewModel(Repository repository, RepositoryNavigationRequest request, Commit commit, bool fillParents)
            : base(new RepositoryNavigationRequest(request) { Treeish = commit.Hash })
        {
            if (fillParents)
            {
                Parents = new List<CommitViewModel>(commit.Parents.Select(x => 
                    new CommitViewModel(repository, request, x, false)));
            }
            Tree = new TreeNodeViewModel(repository, request, commit.Tree);
            Commit = commit;
        }
    }
}
