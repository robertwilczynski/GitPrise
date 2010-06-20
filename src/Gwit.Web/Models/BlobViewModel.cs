using System;
using GitSharp;
using System.Collections.Generic;

namespace Gwit.Web.Models
{
    public class BlobViewModel : RepositoryNavigationViewModelBase
    {
        public string Path { get; private set; }
        
        public PathViewModel PathModel { get; private set; }

        public Leaf Blob { get; private set; }
        
        public string FormattedData { get; set; }

        public BlobViewModel(Repository repository, string repositoryName, string treeish, Leaf blob, PathViewModel pathModel)
            : base(repository, repositoryName, treeish)
        {
            Blob = blob;
            Path = blob.Path;
            PathModel = pathModel;
        }
    }
}
