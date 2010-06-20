using System;

namespace System
{
    public static class StringExtensions
    {
        public static string Fill(this string @this, params object[] args)
        {
            return String.Format(@this, args);
        }
    }
}
