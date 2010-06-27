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

namespace GitPrise.Web.Models
{
    public class ListItemViewModel : RepositoryNavigationRequest, IComparable
    {
        public enum ItemType
        {
            Blob,
            Tree
        }

        public string Name { get; set; }
        public string Message { get; set; }
        public string Author { get; set; }
        public ItemType Type { get; set; }
        public DateTimeOffset? AuthorDate { get; set; }
        public DateTimeOffset? CommitDate { get; set; }

        public ListItemViewModel(RepositoryNavigationRequest request)
            : base(request)
        {
            
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj", "obj is null.");
            }

            if (!(obj is ListItemViewModel))
            {
                throw new ArgumentNullException("obj", "obj is not ListItemViewModel.");
            }

            return this.Name.CompareTo((obj as ListItemViewModel).Name);
        }
    }
}
