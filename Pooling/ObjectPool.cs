using System;
using System.Collections;
using System.Collections.Generic;
using Godot;

namespace Util.ObjectPool
{
    public class ObjectPool<T> : IObjectPool<T> where T : class, IObjectPoolItem<T>, new()
    {
        private readonly Queue<T> pool = new();

        public int Count => pool.Count;

        public ObjectPool(int initialSize = 0)
        {
            for (int i = 0; i < initialSize; i++)
                InstantiateObject();
        }

        protected virtual T InstantiateObject()
        {
            var item = new T();
            pool.Enqueue(item);
            item.OnCreate(this);
            return item;
        }

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
}