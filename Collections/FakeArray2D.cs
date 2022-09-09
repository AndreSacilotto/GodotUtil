using System;
using System.Collections;
using System.Collections.Generic;

namespace Util.Collections
{
    public class FakeArray2D<T> : ICloneable, ICollection, IStructuralComparable, IStructuralEquatable, IReadOnlyCollection<T>
    {
        protected T[] array;

        public FakeArray2D(int size) : this(size, size) { }
        public FakeArray2D(int rows, int collumns)
        {
            if (rows <= 0 || collumns <= 0)
                throw new Exception("Array Size is less than 0");

            SetSize(rows, collumns);
        }

        /// <summary>Also means Width or X</summary>
        public int Rows => rows;
        private int rows;

        /// <summary>Also means Height or Y</summary>
        public int Collumns => collumns;
        private int collumns;

        public int Count => array.Length;
        public object SyncRoot => array.SyncRoot;
        public bool IsSynchronized => array.IsSynchronized;

        protected void SetSize(int rows, int collumns)
        {
            this.rows = rows;
            this.collumns = collumns;
            array = new T[collumns * rows];
        }

        public void Resize(int newSize) => Resize(newSize, newSize);
        public void Resize(int newRows, int newCollumns)
        {
            if (newCollumns < 0 || newRows < 0)
                return;

            var oldArray = array;
            int rMin = Math.Min(rows, newRows);
            int cMin = Math.Min(collumns, newCollumns);
            SetSize(newRows, newCollumns);

            for (int r = 0; r < rMin; r++)
            {
                int rr = r * rows;
                for (int c = 0; c < cMin; c++)
                    this[r, c] = oldArray[rr + c];
            }
        }

        public IEnumerator<T> ColumnMajor()
        {
            for (int i = 0; i < Count; i++)
                yield return this[i];
        }

        public IEnumerator<T> RowMajor()
        {
            for (int r = 0; r < rows; r++)
            {
                int rr = r * rows;
                for (int c = 0; c < collumns; c++)
                    yield return this[rr + c];
            }
        }

        public T this[int index]
        {
            get => array[index];
            set => array[index] = value;
        }
        public T this[int row, int collumn]
        {
            get => array[row * rows + collumn];
            set => array[row * rows + collumn] = value;
        }

        public object Clone() => array.Clone();
        public void CopyTo(Array arr, int index) => array.CopyTo(arr, index);
        public int CompareTo(object other, IComparer comparer) => ((IStructuralComparable)array).CompareTo(other, comparer);
        public bool Equals(object other, IEqualityComparer comparer) => ((IStructuralEquatable)array).Equals(other, comparer);
        public int GetHashCode(IEqualityComparer comparer) => ((IStructuralEquatable)array).GetHashCode(comparer);

        IEnumerator IEnumerable.GetEnumerator() => array.GetEnumerator();
        public IEnumerator<T> GetEnumerator() => (IEnumerator<T>)array.GetEnumerator();

    }
}
