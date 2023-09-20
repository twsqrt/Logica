using System.Collections.Generic;
using System.Linq;


namespace Extensions
{
    public static class EnumerableExtesnsion
    {
        public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(this IEnumerable<IEnumerable<T>> sequence)
        {
            IEnumerable<IEnumerable<T>> result = new[]{ Enumerable.Empty<T>() };

            foreach(IEnumerable<T> set in sequence)
            {
                result = 
                    from r in result
                    from s in set
                    select r.Concat( new[]{s} );
            } 

            return result;
        }

        public static IEnumerable<IEnumerable<T>> InPower<T>(this IEnumerable<T> enumerable, int n)
            => Enumerable.Repeat(enumerable, n).CartesianProduct();
    }
}