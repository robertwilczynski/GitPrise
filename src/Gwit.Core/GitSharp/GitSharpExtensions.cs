using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GitSharp.Core;

namespace GitSharp
{
    public static class GitSharpExtensions
    {

        /// <summary>
        /// Find the commit this file or tree was last changed in
        /// </summary>
        public static Commit GetLastChangingCommit(this AbstractTreeNode node, Commit commit)
        {
            if (commit == null)
                return null;
            foreach (var c in new[] { commit }.Concat(commit.Ancestors))
            {
                foreach (var change in c.Changes)
                {
                    if (change.Path == node.Name) // Todo: here, renaming of this file should be detected and tracked
                    {
                        return c;
                    }                    
                    // TODO: optimize to not search any further if we find the point of creation of this file
                }
            }
            return null;
        }

    }

}