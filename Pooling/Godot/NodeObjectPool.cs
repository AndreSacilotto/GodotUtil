using System.Collections.Generic;
using Godot;

namespace Util.ObjectPool
{
	public class NodeObjectPool<T> : ObjectPool<T> where T : class, IObjectPoolItem<T>, new()
	{
		protected PackedScene poolObject;

		private readonly Queue<T> pool = new();

		public NodeObjectPool(PackedScene poolObject, int initialSize = 0) : base(initialSize)
		{
			this.poolObject = poolObject;
		}

		protected override T InstantiateObject()
		{
			var item = poolObject.Instance<T>();
			pool.Enqueue(item);
			item.OnCreate(this);
			return item;
		}

	}
}