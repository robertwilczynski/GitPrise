using System;
using GitSharp;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using System.Linq;

namespace Gwit.Web.Models
{
    public class PathViewModel : RepositoryNavigationViewModelBase
    {
        public Element Root { get; private set; }
        public IList<Element> Elements { get; private set; }
        public Element CurrentItem { get; private set; }
        public bool IsRootEqualToCurrentItem { get; private set; }

        public PathViewModel(Repository repository, RequestContext context, string repositoryName, string id, AbstractTreeNode node)
            : base(repository, repositoryName, id)
        {
            Elements = new List<Element>();

            CurrentItem = new Element(context, repositoryName, id, node);


            var parts = node.Path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var elementParts = parts.Take(parts.Length - 1);

            var currentNode = node;            
            //for (int i = elementParts.Length - 1; i >= 0; i--)
            //{
            //    var part = parts[i];
            //    currentNode.pa
            //    if (part is Leaf)
            //}

            //var treeParts = parts.Take(parts.Length - 1);
            //var blobName = parts.Last();
            //foreach (string part in treeParts)
            //{
            //    var trees = tree.Trees.ToList();
            //    tree = trees.First(x => x.Name == part);
            //}
            //var blob = tree.Leaves.First(x => x.Name == blobName);

            //var viewModel = new BlobViewModel(repositoryName, commit, blob, 
            //    new PathViewModel(this.Request.RequestContext, repositoryName, id, blob))
            //    {
            //        FormattedData = _hightlightingService.GenerateHtml(blob.Data, blob.Path, null)
            //    };
            //return View(viewModel);

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

            public Element(RequestContext context, string repositoryName, string id, AbstractTreeNode node)
            {
                var helper = new UrlHelper(context);

                Text = !String.IsNullOrEmpty(node.Name) ? node.Name : repositoryName;
                Url = helper.Action("Tree", "Repository", new
                {
                    repositoryName = repositoryName,
                    id = id,
                    path = node.Path
                });
            }
        }
    }
}
