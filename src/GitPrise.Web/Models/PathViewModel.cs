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

            CurrentItem = new Element(request, repositoryName, id, node);

            var currentNode = node;

            while (currentNode.Parent != null)
            {
                currentNode = currentNode.Parent;
                if (currentNode.Parent != null)
                {
                    Elements.Add(new Element(request, repositoryName, id, currentNode));
                }
            }

            Elements = new List<Element>(Elements.Reverse());
            Root = new Element(request, repositoryName, id, currentNode);
            IsRootEqualToCurrentItem = (currentNode == node);
        }

        public class Element
        {
            public string Text { get; set; }
            public string Url { get; set; }
            public string IsNavigationElement { get; set; }

            public Element(RepositoryNavigationRequest request, string repositoryName, string treeish, AbstractTreeNode node)
            {
                var helper = new UrlHelper(HttpContext.Current.Request.RequestContext);

                Text = !String.IsNullOrEmpty(node.Name) ? node.Name : repositoryName;
                Url = helper.Action("tree", "Repository", new
                {
                    repositoryName = repositoryName,
                    id = treeish,
                    path = node.Path,
                    location = request.RepositoryLocation,
                });
            }
        }
    }
}
