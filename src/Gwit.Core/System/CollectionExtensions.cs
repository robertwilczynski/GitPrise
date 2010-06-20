using System;
using System.Collections.Generic;

namespace System
{
    public static class CollectionExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> @this, Action<T> action)
        {
            foreach (var item in @this)
            {
                action(item);
            }
        }
    }
}
