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
using System.Web.Routing;
using GitPrise.Core.Web.Mvc;

namespace GitPrise.Web.Models
{
    public class BlobViewModel : RepositoryNavigationViewModelBase
    {
        public Leaf Blob { get; private set; }

        public string FormattedData { get; set; }

        public BlobViewModel(Repository repository, RepositoryNavigationRequest request, Leaf blob)
            : this(repository,
                request.RepositoryName,
                request.Treeish,
                blob,
                new PathViewModel(request, blob))
        {
        }
        public BlobViewModel(Repository repository, string repositoryName,
            string treeish, Leaf blob, PathViewModel pathModel)
            : base(repository, repositoryName, treeish)
        {
            Blob = blob;
            Path = blob.Path;
            PathModel = pathModel;
        }
    }
}
