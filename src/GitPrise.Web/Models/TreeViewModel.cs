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
            : base(repository, request)
        {
            PathModel = new PathViewModel(request, tree);
            Tree = tree;
            Path = tree.Path;
            IsRoot = Tree.IsRoot;

            Items = new List<ListItemViewModel>();
            foreach (var child in Tree.Children)
            {
                ListItemViewModel.ItemType type;
                AbstractTreeNode node = child as AbstractTreeNode;
                if (child is Leaf)
                {
                    type = ListItemViewModel.ItemType.Blob;
                }
                else if (child is Tree)
                {
                    type = ListItemViewModel.ItemType.Tree;
                }
                else
                {
                    throw new InvalidOperationException("Unexpected child in tree.");
                }

                Commit lastCommit = null;// node.GetLastCommitBefore(commit);
                Items.Add(new ListItemViewModel(request)
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