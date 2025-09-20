namespace Util.Classes;

public class ComparerReverse<T> : IComparer<T> where T : IComparable<T>
{
    public static ComparerReverse<T> Default { get; } = new();

    public int Compare(T? x, T? y) => Comparer<T>.Default.Compare(y, x);
}

public class ComparerInverter<T> : IComparer<T>
{
    private readonly IComparer<T> comparer;
    public ComparerInverter(IComparer<T> comparer)
    {
        this.comparer = comparer;
    }
    public int Compare(T? a, T? b) => comparer.Compare(b, a);
}