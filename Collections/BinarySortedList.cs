using System;
using System.Collections;
using System.Collections.Generic;

namespace Util.Collections
{
    public class BinarySortedList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>
    {
        protected readonly List<T> internalList;
        protected Comparer<T> internalComparer;

        #region Constructors

        protected BinarySortedList(List<T> list, Comparer<T> comparer) 
        {
            internalList = list;
            internalComparer = comparer;
        }

        public BinarySortedList() : this(Comparer<T>.Default) { }
        public BinarySortedList(Comparer<T> comparer) : this(new List<T>(), comparer) { }

        public BinarySortedList(int capacity) : this(capacity, Comparer<T>.Default) { }
        public BinarySortedList(int capacity, Comparer<T> comparer) : this(new List<T>(capacity), comparer) { }

        public BinarySortedList(IEnumerable<T> collection) : this(collection, Comparer<T>.Default) { }
        public BinarySortedList(IEnumerable<T> collection, Comparer<T> comparer) : this(new List<T>(collection), comparer) { }

        #endregion

        public Comparer<T> Comparer { 
            get => internalComparer;
            set {
                internalComparer = value;
                internalList.Sort(internalComparer);
            }
        }

        public int Count => internalList.Count;

        public void Add(T item)
        {
            var index = internalList.BinarySearch(item, internalComparer);
            if (index < 0)
                index = ~index;
            internalList.Insert(index, item);
        }

        public bool Contains(T item) => IndexOf(item) != -1;

        public int IndexOf(T item) => internalList.BinarySearch(item, internalComparer);

        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index >= 0)
            {
                internalList.RemoveAt(index);
                return true;
            }
            return false;
        }

        public void RemoveAt(int index) => internalList.RemoveAt(index);

        public void CopyTo(T[] array) => internalList.CopyTo(array);
        public void CopyTo(T[] array, int arrayIndex) => internalList.CopyTo(array, arrayIndex);

        public void Clear() => internalList.Clear();

        public IEnumerator<T> GetEnumerator() => internalList.GetEnumerator();
        public void Insert(int index, T item) => throw new InvalidOperationException("Cannot Insert in a Sorted List");

        public T this[int index] { get => internalList[index]; set => throw new InvalidOperationException("Cannot Set value in a Sorted List"); }

        #region Object Interface Things

        IEnumerator IEnumerable.GetEnumerator() => internalList.GetEnumerator();
        int IList.Add(object value) => ((IList)internalList).Add(value);
        bool IList.Contains(object value) => ((IList)internalList).Contains(value);
        int IList.IndexOf(object value) => ((IList)internalList).IndexOf(value);
        void IList.Insert(int index, object value) => ((IList)internalList).Insert(index, value);
        void IList.Remove(object value) => ((IList)internalList).Remove(value);
        void ICollection.CopyTo(Array array, int index) => ((ICollection)internalList).CopyTo(array, index);

        bool IList.IsReadOnly => ((IList)internalList).IsReadOnly;
        bool ICollection<T>.IsReadOnly => ((ICollection<T>)internalList).IsReadOnly;
        bool IList.IsFixedSize => ((IList)internalList).IsFixedSize;
        object ICollection.SyncRoot => ((ICollection)internalList).SyncRoot;
        bool ICollection.IsSynchronized => ((ICollection)internalList).IsSynchronized;
        
        object IList.this[int index] { get => ((IList)internalList)[index]; set => ((IList)internalList)[index] = value; }

        #endregion

    }

}
