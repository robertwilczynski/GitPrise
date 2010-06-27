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

namespace GitPrise.Web.Models
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