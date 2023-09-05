using System.Collections.Generic;
using System.Linq;

namespace Extensions
{
    public static class EnumerableExtenstion
    {
        public static bool ISEmpty<T>(this IEnumerable<T> enumerable)
            => enumerable.Any() == false;
    }
}