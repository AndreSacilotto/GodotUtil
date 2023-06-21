using Godot;
using Util.Pool;

namespace GodotUtil;

public class NodeObjectPool<T> : ObjectPool<T> where T : Node, IObjectPoolItem<T>
{
    protected PackedScene poolObject;

    public NodeObjectPool(PackedScene poolObject, int initialSize = 0) : base(initialSize)
    {
        this.poolObject = poolObject;
    }

    protected override T InstantiateObject()
    {
        var instance = poolObject.Instantiate<T>();
        instance.OnCreate(this);
        return instance;
    }

}