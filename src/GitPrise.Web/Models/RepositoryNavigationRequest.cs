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
    public class RepositoryNavigationRequest : IEquatable<RepositoryNavigationRequest>
    {
        /// <summary>
        /// Repository name.
        /// </summary>
        /// <remarks>
        /// Also serves as location relative to <see cref="Settings.RepositoryRootPath"/> 
        /// </remarks>
        public string RepositoryName { get; set; }

        public string Treeish { get; set; }
        public string Path { get; set; }

        /// <summary>Ad hoc repository location.</summary>
        /// <remarks>Used when accessing repository outside of configured location (in git instaweb scenarios).</remarks>
        /// <value>Absolute path to repository. Overrides <see cref="RepositoryName"./></value>
        public string RepositoryLocation { get; set; }

        public RepositoryNavigationRequest()
        {

        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="request"></param>
        public RepositoryNavigationRequest(RepositoryNavigationRequest request)
        {
            RepositoryName = request.RepositoryName;
            Treeish = request.Treeish;
            Path = request.Path;
            RepositoryLocation = request.RepositoryLocation;
        }

        public bool Equals(RepositoryNavigationRequest other)
        {
            return (RepositoryName == other.RepositoryName &&
                RepositoryLocation == other.RepositoryLocation &&
                Path == other.Path &&
                Treeish == other.Treeish);
        }
    }
}