using System.Collections;

namespace Util.Classes;

public class ObjectPoolEvents<T> : IObjectPool<T> where T : class
{
    private readonly Queue<T> pool = new();

    public int Count => pool.Count;

    private readonly Func<T> CreateFunc;
    public event Action<T>? OnTakeFromPool;
    public event Action<T>? OnReturnToPool;
    public event Action<T>? OnDestroyFromPool;

    public ObjectPoolEvents(Func<T> createFunc, int initialSize = 0, Action<T>? onTakeFromPool = null, Action<T>? onReturnToPool = null, Action<T>? onDestroyFromPool = null)
    {
        CreateFunc = createFunc;
        OnTakeFromPool = onTakeFromPool;
        OnReturnToPool = onReturnToPool;
        OnDestroyFromPool = onDestroyFromPool;
        for (int i = 0; i < initialSize; i++)
            InstantiateObject();
    }

    private T InstantiateObject()
    {
        var item = CreateFunc();
        pool.Enqueue(item);
        return item;
    }

    public T Request()
    {
        T item = pool.Count > 0 ? pool.Dequeue() : InstantiateObject();
        OnTakeFromPool?.Invoke(item);
        return item;
    }

    public void Return(T item)
    {
        pool.Enqueue(item);
        OnReturnToPool?.Invoke(item);
    }

    public void Clear()
    {
        if (OnDestroyFromPool != null)
            foreach (var item in pool)
                OnDestroyFromPool.Invoke(item);
        pool.Clear();
    }

    public IEnumerator<T> GetEnumerator() => pool.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => pool.GetEnumerator();

}