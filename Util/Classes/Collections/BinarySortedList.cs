using System.Collections;

namespace Util.Classes;

public class BinarySortedList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>
{
    protected readonly List<T> internalList;
    protected IComparer<T> internalComparer;

    #region Constructors

    protected BinarySortedList(List<T> list, IComparer<T> comparer)
    {
        internalList = list;
        internalComparer = comparer;
    }

    public BinarySortedList() : this(Comparer<T>.Default) { }
    public BinarySortedList(IComparer<T> comparer) : this(new List<T>(), comparer) { }

    public BinarySortedList(int capacity) : this(capacity, Comparer<T>.Default) { }
    public BinarySortedList(int capacity, IComparer<T> comparer) : this(new List<T>(capacity), comparer) { }

    public BinarySortedList(IEnumerable<T> collection) : this(collection, Comparer<T>.Default) { }
    public BinarySortedList(IEnumerable<T> collection, IComparer<T> comparer) : this(new List<T>(collection), comparer) { }

    #endregion

    public IComparer<T> Comparer
    {
        get => internalComparer;
        set
        {
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

    void ICollection.CopyTo(Array array, int index) => ((ICollection)internalList).CopyTo(array, index);
    public int Add(object? value) => ((IList)internalList).Add(value);
    public bool Contains(object? value) => ((IList)internalList).Contains(value);
    public int IndexOf(object? value) => ((IList)internalList).IndexOf(value);
    public void Insert(int index, object? value) => ((IList)internalList).Insert(index, value);
    public void Remove(object? value) => ((IList)internalList).Remove(value);

    bool IList.IsReadOnly => ((IList)internalList).IsReadOnly;
    bool ICollection<T>.IsReadOnly => ((ICollection<T>)internalList).IsReadOnly;
    bool IList.IsFixedSize => ((IList)internalList).IsFixedSize;
    object ICollection.SyncRoot => ((ICollection)internalList).SyncRoot;
    bool ICollection.IsSynchronized => ((ICollection)internalList).IsSynchronized;

    object? IList.this[int index] { get => internalList[index]; set => ((IList)internalList)[index] = value; }

    #endregion

}
