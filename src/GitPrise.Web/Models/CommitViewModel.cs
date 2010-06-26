using System;
using GitSharp;
using System.Collections.Generic;
using System.IO;

namespace GitPrise.Web.Models
{
    public class CommitViewModel : RepositoryNavigationViewModelBase
    {
        public List<ChangeViewModel> Changes { get; private set; }

        public CommitViewModel(Repository repository, string name, string treeish)
            : base(repository, name, treeish)
        {
            Changes = new List<ChangeViewModel>();   
        }

        public CommitViewModel(Repository repository, RepositoryNavigationRequest request, Commit commit)
            : this(repository, request.RepositoryName, commit.Hash)
        {
            CurrentCommit = commit;
        }
    }
}
