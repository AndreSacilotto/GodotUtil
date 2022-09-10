using System;
using System.Collections.Generic;

namespace Util
{
    public class ComparerReverse<T> : IComparer<T> where T : IComparable<T>
    {
        public static ComparerReverse<T> Default { get; } = new();

        public int Compare(T x, T y) => y.CompareTo(x);
    }
}