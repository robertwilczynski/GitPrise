using System;
using GitSharp;

namespace GitPrise.Web.Models
{
    public class TreeNodeViewModel : RepositoryNavigationRequest
    {
        public AbstractTreeNode Node { get; set; }
        public TreeNodeViewModel(Repository repository, RepositoryNavigationRequest request, AbstractTreeNode node)
            : base(request)
        {
            Node = node;
        }
    }
}
