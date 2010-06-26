using System;

namespace GitPrise.Web.Models
{
    public class RepositoryNavigationRequest
    {
        /// <summary>
        /// Repository name.
        /// </summary>
        /// <remarks>
        /// Also serves as location relative to <see cref="Settings.RepositoryRootPath"/> 
        /// </remarks>
        public string RepositoryName {get; set;}

        public string Treeish {get; set;}
        public string Path {get; set;}

        /// <summary>Ad hoc repository location.</summary>
        /// <remarks>Used when accessing repository outside of configured location (in git instaweb scenarios).</remarks>
        /// <value>Absolute path to repository. Overrides <see cref="RepositoryName"./></value>
        public string RepositoryLocation {get; set;}
    }
}