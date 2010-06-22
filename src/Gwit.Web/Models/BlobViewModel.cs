using System;
using GitSharp;
using System.Web.Routing;
using Gwit.Core.Web.Mvc;

namespace Gwit.Web.Models
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
