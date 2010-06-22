using System;
using System.Collections.Generic;
using GitSharp;

namespace Gwit.Web.Models
{

    public class TreeViewModel : RepositoryNavigationViewModelBase
    {
        public bool IsRoot { get; set; }
        public Tree Tree { get; set; }
        public List<ListItemViewModel> Items { get; set; }

        public TreeViewModel(Repository repository, RepositoryNavigationRequest request, Tree tree)
            : this(repository,
                request.RepositoryName,
                request.Treeish,
                tree,
                new PathViewModel(request, tree))
        {

        }

        public TreeViewModel(Repository repository, string repositoryName, string treeish, Tree tree, PathViewModel pathModel)
            : base(repository, repositoryName, treeish)
        {
            PathModel = pathModel;
            Tree = tree;
            Path = tree.Path;
            IsRoot = Tree.IsRoot;

            Items = new List<ListItemViewModel>();
            foreach (var child in Tree.Children)
            {
                var type = ListItemViewModel.ItemType.Blob;
                AbstractTreeNode node = child as AbstractTreeNode;
                if (child is Leaf)
                {
                    var childLeaf = (Leaf)child;
                }
                else if (child is Tree)
                {
                    var childTree = (Tree)child;
                    type = ListItemViewModel.ItemType.Tree;
                }
                //var change = null; // (child as ITreeNode).FindLastCommit(commit);
                Commit lastCommit = null;// node.GetLastCommitBefore(commit);
                Items.Add(new ListItemViewModel
                {
                    Name = node.Name,
                    Author = lastCommit != null ? lastCommit.Author.Name : String.Empty,
                    AuthorDate = lastCommit != null ? lastCommit.AuthorDate : (DateTimeOffset?)null,
                    CommitDate = lastCommit != null ? lastCommit.CommitDate : (DateTimeOffset?)null,
                    Message = lastCommit != null ? lastCommit.Message : String.Empty,
                    Type = type,
                    Path = node.Path,
                });
            }
        }
    }
}