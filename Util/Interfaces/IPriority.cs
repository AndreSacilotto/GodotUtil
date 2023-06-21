namespace Util;

public interface IPriority
{
    int Priority { get; }
}

public class PriorityComparerLowerFirst : IComparer<IPriority>
{
    public static PriorityComparerLowerFirst Default { get; } = Create();
    public static PriorityComparerLowerFirst Create() => new();
    public int Compare(IPriority? x, IPriority? y)
    {
        if (x == null || y == null)
            return 1;
        return y.Priority.CompareTo(x.Priority);
    }
}

public class PriorityComparerHigherFirst : IComparer<IPriority>
{
    public static PriorityComparerHigherFirst Default { get; } = Create();
    public static PriorityComparerHigherFirst Create() => new();
    public int Compare(IPriority? x, IPriority? y)
    {
        if (x == null || y == null)
            return 1;
        return y.Priority.CompareTo(x.Priority);
    }
}