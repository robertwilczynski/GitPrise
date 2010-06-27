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
using System.Web.Mvc;
using System.Linq;
using System.Web;

namespace GitPrise.Web.Models
{
    public class PathViewModel
    {
        public Element Root { get; private set; }
        public IList<Element> Elements { get; private set; }
        public Element CurrentItem { get; private set; }
        public bool IsRootEqualToCurrentItem { get; private set; }

        public PathViewModel(RepositoryNavigationRequest request, AbstractTreeNode node)
            : this(request,
                request.RepositoryName,
                request.Treeish,
                node)
        {
        }

        public PathViewModel(RepositoryNavigationRequest request, string repositoryName, string id, AbstractTreeNode node)
        {
            Elements = new List<Element>();

            CurrentItem = new Element(new RepositoryNavigationRequest(request) { Path = node.Path }, repositoryName, id, node);

            var currentNode = node;

            while (currentNode.Parent != null)
            {
                currentNode = currentNode.Parent;
                if (currentNode.Parent != null)
                {
                    Elements.Add(new Element(new RepositoryNavigationRequest(request) { Path = currentNode.Path }, repositoryName, id, currentNode));
                }
            }

            Elements = new List<Element>(Elements.Reverse());
            Root = new Element(request, repositoryName, id, currentNode);
            IsRootEqualToCurrentItem = (currentNode == node);
        }

        public class Element
        {
            public string Text { get; set; }
            //public string Url { get; set; }
            //public string IsNavigationElement { get; set; }
            public RepositoryNavigationRequest Navigation { get; set; }

            public Element(RepositoryNavigationRequest request, string repositoryName, string treeish, AbstractTreeNode node)
            {
                //var helper = new UrlHelper(HttpContext.Current.Request.RequestContext);

                Text = !String.IsNullOrEmpty(node.Name) ? node.Name : repositoryName;
                Navigation = new RepositoryNavigationRequest
                {
                    RepositoryName = request.RepositoryName,
                    Path = node.Path,
                    Treeish = treeish,
                    RepositoryLocation = request.RepositoryLocation,
                };
                //Url = helper.Action("tree", "Repository", new
                //{
                //    repositoryName = repositoryName,
                //    id = treeish,
                //    path = node.Path,
                //    location = request.RepositoryLocation,
                //});
            }
        }
    }
}
