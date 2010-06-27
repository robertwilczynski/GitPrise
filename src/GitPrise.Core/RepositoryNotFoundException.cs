using System;

namespace GitPrise.Core
{
    public class RepositoryNotFoundException : Exception
    {
        public string RepositoryLocation { get; set; }

        public RepositoryNotFoundException(string repositoryName)
            : base(string.Format("Repository '{0}' not found.", repositoryName))
        {
            
        }
    }
}
