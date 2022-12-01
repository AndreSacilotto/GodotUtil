namespace Util.ObjectPool
{
	public interface IObjectPoolItem<T> where T : class
	{
		IObjectPool<T> PoolOwner { get; }

		void OnCreate(IObjectPool<T> pool);

		void OnTakeFromPool();

		void OnReturnToPool();

		void OnDestroyFromPool();
	}
}