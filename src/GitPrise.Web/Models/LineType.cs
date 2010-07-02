using System;

namespace GitPrise.Web.Models
{
    public enum LineType
    {
        Unchanged,
        Inserted,
        Removed,

        /// <summary>
        /// Special purpose line used to indicate lines were omitted. Used when diff is compacted.
        /// </summary>
        Ellipsis,
    }
}
