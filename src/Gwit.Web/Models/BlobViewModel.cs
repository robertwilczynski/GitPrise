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

        public BlobViewModel(Repository repository, RequestContext context, Leaf blob)
            : this(repository,
                context.GetRepositoryName(),
                context.GetTreeish(),
                blob,
                new PathViewModel(context, blob))
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
