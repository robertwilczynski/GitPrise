using System;
using System.Linq;

namespace GitSharp
{
    public static class GitSharpExtensions
    {
        public static AbstractTreeNode Node(this Tree tree, string path)
        {
            if (String.IsNullOrEmpty(path))
            {
                return tree;
            }

            var parts = path.Split('/');
            AbstractTreeNode node = tree;
            foreach (string part in parts)
            {

                node = (node as Tree).Children.FirstOrDefault(x => ((AbstractTreeNode)x).Name == part) as AbstractTreeNode;
            }
            return node;
        }
    }

}