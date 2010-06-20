using System;
using GitSharp;
using System.Web.Routing;
using Gwit.Core.Web.Mvc;

namespace Gwit.Web.Models
{
    public class CommitViewModel : RepositoryNavigationViewModelBase
    {
        public CommitViewModel(Repository repository, string name, string treeish)
            : base(repository, name, treeish)
        {
            
        }
        public CommitViewModel(Repository repository, RequestContext context, Commit commit)
            : this(repository, context.GetRepositoryName(), commit.Hash)
        {
            CurrentCommit = commit;
        }

    }
}
