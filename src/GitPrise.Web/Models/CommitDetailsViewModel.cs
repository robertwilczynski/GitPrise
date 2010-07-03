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
using System.Text;

namespace GitPrise.Web.Models
{
    public class CommitDetailsViewModel : RepositoryNavigationViewModelBase
    {
        public List<ChangeViewModel> Changes { get; private set; }

        private CommitDetailsViewModel(Repository repository, RepositoryNavigationRequest request)
            : base(repository, request)
        {
            Changes = new List<ChangeViewModel>();
        }

        public CommitDetailsViewModel(Repository repository, RepositoryNavigationRequest request, Commit commit)
            : this(repository, new RepositoryNavigationRequest(request) { Treeish = commit.Hash })
        {
            CurrentCommit = new CommitViewModel(repository, request, commit, true);

            foreach (var change in commit.Changes)
            {
                // PASTE-START : borrowed from GitSharp.Demo
                var a = (change.ReferenceObject != null ? (change.ReferenceObject as Blob).RawData : new byte[0]);
                var b = (change.ComparedObject != null ? (change.ComparedObject as Blob).RawData : new byte[0]);

                a = (Diff.IsBinary(a) == true ? Encoding.ASCII.GetBytes("Binary content\nFile size: " + a.Length) : a);
                b = (Diff.IsBinary(b) == true ? Encoding.ASCII.GetBytes("Binary content\nFile size: " + b.Length) : b);
                // PASTE-END : borrowed from GitSharp.Demo

                var diff = new Diff(a, b);
                Changes.Add(new ChangeViewModel(request, change, diff));
            }
        }
    }
}
