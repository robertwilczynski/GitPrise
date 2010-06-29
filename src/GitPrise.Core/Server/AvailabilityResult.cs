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

namespace GitPrise.Core.Server
{
    public enum AvailabilityResult
    {

        /// <summary>
        /// Port might be in use by other application.
        /// </summary>
        Unknown,

        /// <summary>
        /// Port is available.
        /// </summary>
        Available,

        /// <summary>
        /// Port is in use by GitPrise (server already running).
        /// </summary>
        InUseByGitPrise
    }
}
