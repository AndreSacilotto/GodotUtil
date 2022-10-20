using System;
using System.Collections.Generic;

namespace Util
{
    public class ComparerReverse<T> : IComparer<T> where T : IComparable<T>
    {
        public static IComparer<T> Default { get; } = new ComparerReverse<T>();

        public int Compare(T x, T y) => y.CompareTo(x);
    }
}