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
using System.Web.Routing;
using GitPrise.Core.Web.Mvc;

namespace GitPrise.Web.Models
{
    public class CommitsViewModel : RepositoryNavigationViewModelBase
    {
        private List<Commit> List { get; set; }

        public IList<IGrouping<DateTime, Commit>> Grouping { get; private set; }

        public void AddCommit(Commit commit)
        {
            List.Add(commit);
        }

        public void AddCommits(IList<Commit> commits)
        {
            List.AddRange(commits);
        }

        public CommitsViewModel(Repository repository, RepositoryNavigationRequest request)
            : base(repository, request)
        {
            List = new List<Commit>();
        }

        public void ApplyGrouping()
        {
            Grouping = new List<IGrouping<DateTime, Commit>>(
                List
                    .GroupBy(x => x.CommitDate.Date)
                    .OrderByDescending(x => x.Key)
            );                
        }
    }

    
}
