using System;
using System.Collections.Generic;

public class ComparerReverse<T> : IComparer<T> where T : IComparable<T>
{
    public int Compare(T x, T y) => y.CompareTo(x);
}

