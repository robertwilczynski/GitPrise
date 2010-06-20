using System;
using GitSharp;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using System.Linq;
using Gwit.Core.Web.Mvc;

namespace Gwit.Web.Models
{
    public class PathViewModel
    {
        public Element Root { get; private set; }
        public IList<Element> Elements { get; private set; }
        public Element CurrentItem { get; private set; }
        public bool IsRootEqualToCurrentItem { get; private set; }

        public PathViewModel(RequestContext context, AbstractTreeNode node)
            : this(context,
                context.GetRepositoryName(),
                context.GetTreeish(),
                node)
        {
        }

        public PathViewModel(RequestContext context, string repositoryName, string id, AbstractTreeNode node)
        {
            Elements = new List<Element>();

            CurrentItem = new Element(context, repositoryName, id, node);

            var parts = node.Path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var currentNode = node;

            while (currentNode.Parent != null)
            {
                currentNode = currentNode.Parent;
                if (currentNode.Parent != null)
                {
                    Elements.Add(new Element(context, repositoryName, id, currentNode));
                }
            }

            Elements = new List<Element>(Elements.Reverse());
            Root = new Element(context, repositoryName, id, currentNode);
            IsRootEqualToCurrentItem = (currentNode == node);
        }

        public class Element
        {
            public string Text { get; set; }
            public string Url { get; set; }
            public string IsNavigationElement { get; set; }

            public Element(RequestContext context, string repositoryName, string treeish, AbstractTreeNode node)
            {
                var helper = new UrlHelper(context);

                Text = !String.IsNullOrEmpty(node.Name) ? node.Name : repositoryName;
                Url = helper.Action("Tree", "Repository", new
                {
                    repositoryName = repositoryName,
                    id = treeish,
                    path = node.Path,
                    location = context.GetRepositoryLocation(),
                });
            }
        }
    }
}
