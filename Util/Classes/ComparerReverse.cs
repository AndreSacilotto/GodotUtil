namespace Util;

public class ComparerReverse<T> : IComparer<T> where T : IComparable<T>
{
	public static ComparerReverse<T> Default { get; } = Create();
	public static ComparerReverse<T> Create() => new();

	public int Compare(T? x, T? y)
	{
		if (x == null)
			return 1;
		return x.CompareTo(y);
	}

}