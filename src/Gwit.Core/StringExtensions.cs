using System;

namespace Gwit.Core
{
    public static class StringExtensions
    {
        private static readonly string GitRepositoryPostfix = ".git";
        public static string Git(this string @this)
        {
            if (!@this.EndsWith(GitRepositoryPostfix))
            {
                return @this + GitRepositoryPostfix;
            } 
            return @this;
        }

        public static string TrimGit(this string @this)
        {
            if (@this.EndsWith(GitRepositoryPostfix))
            {
                return @this.Remove(@this.Length - GitRepositoryPostfix.Length);
            }
            return @this;
        }

    }
}
