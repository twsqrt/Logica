using System.Collections.Generic;

namespace Extensions
{
    public static class CompareExtension
    {
        public static bool IsBetween<T>(this T value, T left, T right)
        {
            return Comparer<T>.Default.Compare(value, left) >= 0
                && Comparer<T>.Default.Compare(value, right) <= 0;
        }
    }
}