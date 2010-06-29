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
