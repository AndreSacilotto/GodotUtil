using System.Collections;

namespace Util.Pool;

public abstract class ObjectPool<T> : IObjectPool<T> where T : class, IObjectPoolItem<T>
{
	private readonly Queue<T> pool = new();

	public int Count => pool.Count;

	public ObjectPool(int initialSize = 0)
	{
		for (int i = 0; i < initialSize; i++)
			pool.Enqueue(InstantiateObject());
	}

	protected abstract T InstantiateObject();

	public T Request()
	{
		T item = pool.Count > 0 ? pool.Dequeue() : InstantiateObject();
		item.OnTakeFromPool();
		return item;
	}

	public void Return(T item)
	{
		pool.Enqueue(item);
		item.OnReturnToPool();
	}

	public void Clear()
	{
		foreach (var item in pool)
			item.OnDestroyFromPool();
		pool.Clear();
	}

	public IEnumerator<T> GetEnumerator() => pool.GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}