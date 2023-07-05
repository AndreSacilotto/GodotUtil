namespace Util.Classes;

public interface IObjectPool<T> : IEnumerable<T> where T : class
{
    int Count { get; }
    T Request();
    void Clear();
    void Return(T item);
}