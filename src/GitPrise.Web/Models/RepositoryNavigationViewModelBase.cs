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
    public class RepositoryNavigationViewModelBase : RepositoryNavigationRequest
    {
        public CommitViewModel CurrentCommit { get; set; }
        public List<BranchViewModel> Branches { get; set; }
        public List<BranchViewModel> Tags { get; set; }
        public PathViewModel PathModel { get; protected set; }

        virtual public string Title
        {
            get
            {
                if (!String.IsNullOrEmpty(Path))
                {
                    return "{0} at {1} from {2}".Fill(Path, Treeish, RepositoryName);
                }
                return "{0} at {1}".Fill(RepositoryName, Treeish);
            }
        }

        private RepositoryNavigationViewModelBase()
        {
            Branches = new List<BranchViewModel>();
            Tags = new List<BranchViewModel>();
        }

        public RepositoryNavigationViewModelBase(Repository repository, RepositoryNavigationRequest request)
            : this()
        {
            RepositoryName = request.RepositoryName;
            Treeish = request.Treeish;
            Path = request.Path;
            RepositoryLocation = request.RepositoryLocation;
            FillFromRepository(repository, request);
        }

        public void FillFromRepository(Repository repository, RepositoryNavigationRequest request)
        {
            Branches.AddRange(repository.Branches.Keys.Select(x =>
                new BranchViewModel(x, new RepositoryNavigationRequest()
                {
                    RepositoryName = request.RepositoryName,
                    Treeish = x,
                    RepositoryLocation = request.RepositoryLocation
                })));

            Tags.AddRange(repository.Tags.Keys.Select(x =>
                new BranchViewModel(x, new RepositoryNavigationRequest()
                {
                    RepositoryName = request.RepositoryName,
                    Treeish = x,
                    RepositoryLocation = request.RepositoryLocation
                })));

            var obj = repository.Get<AbstractObject>(Treeish);

            if (obj.IsTag)
            {
                obj = (obj as Tag).Target;
            }

            if (obj.IsCommit)
            {
                CurrentCommit = new CommitViewModel(repository, request, obj as Commit, true);
            }
        }
    }
}
