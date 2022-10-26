using System.Collections;
using System.Collections.Generic;

namespace Util
{
	/// <summary>
	/// https://stackoverflow.com/questions/3663613/why-is-there-no-sortedlistt-in-net
	/// </summary>
	public class SortedCollection<T> : ICollection<T>, IEnumerable<T>, IEnumerable, IReadOnlyList<T>, IReadOnlyCollection<T>
	{
		private List<T> internalList;
		private IComparer<T> internalComparer;

		#region Constructors

		protected SortedCollection(List<T> list, IComparer<T> comparer)
		{
			internalList = list;
			internalComparer = comparer;
		}

		public SortedCollection() : this(Comparer<T>.Default) { }
		public SortedCollection(IComparer<T> comparer) : this(new List<T>(), comparer) { }

		public SortedCollection(int capacity) : this(capacity, Comparer<T>.Default) { }
		public SortedCollection(int capacity, IComparer<T> comparer) : this(new List<T>(capacity), comparer) { }

		public SortedCollection(IEnumerable<T> collection) : this(collection, Comparer<T>.Default) { }
		public SortedCollection(IEnumerable<T> collection, IComparer<T> comparer) : this(new List<T>(collection), comparer) { }

		#endregion

		public IComparer<T> Comparer
		{
			get => internalComparer;
			set {
				internalComparer = value;
				internalList.Sort(internalComparer);
			}
		}

		public int Count => internalList.Count;
		public bool IsReadOnly => false;

		public void Add(T item)
		{
			int insertIndex = FindIndexForSortedInsert(item);
			internalList.Insert(insertIndex, item);
		}

		public bool Contains(T item) => IndexOf(item) != -1;

		/// <summary>
		/// Searches for the specified object and returns the zero-based index of the first occurrence within the entire SortedList<T>
		/// </summary>
		public int IndexOf(T item)
		{
			int insertIndex = FindIndexForSortedInsert(item);
			if (insertIndex == internalList.Count)
				return -1;
			if (internalComparer.Compare(item, internalList[insertIndex]) == 0)
			{
				int index = insertIndex;
				while (index > 0 && internalComparer.Compare(item, internalList[index - 1]) == 0)
					index--;
				return index;
			}
			return -1;
		}

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

		public T this[int index] => internalList[index];

		IEnumerator IEnumerable.GetEnumerator() => internalList.GetEnumerator();
		public IEnumerator<T> GetEnumerator() => internalList.GetEnumerator();


		public int FindIndexForSortedInsert(T item)
		{
			if (internalList.Count == 0)
				return 0;

			int lowerIndex = 0;
			int upperIndex = internalList.Count - 1;
			int comparisonResult;
			while (lowerIndex < upperIndex)
			{
				int middleIndex = (lowerIndex + upperIndex) / 2;
				var middle = internalList[middleIndex];
				comparisonResult = internalComparer.Compare(middle, item);
				if (comparisonResult == 0)
					return middleIndex;
				else if (comparisonResult > 0) // middle > item
					upperIndex = middleIndex - 1;
				else // middle < item
					lowerIndex = middleIndex + 1;
			}

			// At this point any entry following 'middle' is greater than 'item',
			// and any entry preceding 'middle' is lesser than 'item'.
			// So we either put 'item' before or after 'middle'.
			comparisonResult = internalComparer.Compare(internalList[lowerIndex], item);
			return comparisonResult < 0 ? lowerIndex + 1 : lowerIndex;
		}
	}
}