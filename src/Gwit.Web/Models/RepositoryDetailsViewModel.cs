using System;
using GitSharp;

namespace Gwit.Web.Models
{
    public class RepositoryDetailsViewModel : RepositoryNavigationViewModelBase
    {
        public TreeViewModel DefaultTree { get; set; }

        public RepositoryDetailsViewModel(Repository repository, string repositoryName, Commit currentCommit, TreeViewModel defaultTree)
            : base(repository, repositoryName, currentCommit.Hash)
        {
            CurrentCommit = currentCommit;
            DefaultTree = defaultTree;
        }
    }
}