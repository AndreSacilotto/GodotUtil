namespace Util.Classes;

public abstract class ObjectPoolSimple<T> : ObjectPool<T> where T : class, IObjectPoolItem<T>, new()
{
    public ObjectPoolSimple(int initialSize = 0) : base(initialSize)
    {
    }

    protected override T InstantiateObject()
    {
        var item = new T();
        item.OnCreate(this);
        return item;
    }
}