using System;
using GitSharp;
using System.Collections.Generic;

namespace Gwit.Web.Models
{
    public class BlobViewModel : RepositoryNavigationViewModelBase
    {
        private Commit _commit;

        public string Path { get; private set; }
        
        public PathViewModel PathModel { get; private set; }

        public Leaf Blob { get; private set; }
        
        public string FormattedData { get; set; }

        public BlobViewModel(Repository repository, string repositoryName, Commit commit, Leaf blob, PathViewModel pathModel)
            : base(repository, repositoryName, commit.Hash)
        {
            _commit = commit;
            Blob = blob;
            Path = blob.Path;
            PathModel = pathModel;
        }
    }
}
