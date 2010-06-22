using System;
using GitSharp;

namespace Gwit.Web.Models
{
    public class CommitViewModel : RepositoryNavigationViewModelBase
    {
        public CommitViewModel(Repository repository, string name, string treeish)
            : base(repository, name, treeish)
        {
            
        }
        public CommitViewModel(Repository repository, RepositoryNavigationRequest request, Commit commit)
            : this(repository, request.RepositoryName, commit.Hash)
        {
            CurrentCommit = commit;
        }

    }
}
