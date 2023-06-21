namespace Util;

public interface IMin<T>
{
    T Min { get; set; }
}

public interface IMax<T>
{
    T Max { get; set; }
}

public interface IMinMax<T> : IMin<T>, IMax<T> { }