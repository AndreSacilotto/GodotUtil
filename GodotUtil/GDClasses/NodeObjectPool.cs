using Godot;

namespace Util.Pool
{
	public class NodeObjectPool<T> : ObjectPool<T> where T : Node, IObjectPoolItem<T>
	{
		protected PackedScene poolObject;

		public NodeObjectPool(PackedScene poolObject, int initialSize = 0) : base(initialSize)
		{
			this.poolObject = poolObject;
		}

		protected override T InstantiateObject()
		{
			var item = poolObject.Instance<T>();
			item.OnCreate(this);
			return item;
		}

	}
}