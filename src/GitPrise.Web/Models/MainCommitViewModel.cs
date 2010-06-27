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
