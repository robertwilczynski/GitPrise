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