using System.Runtime.CompilerServices;

namespace Util;

public static class UtilCollection
{

    #region Array

    public static T[,] NewOfSameSize<T>(this T[,] array) => new T[array.GetLength(0), array.GetLength(1)];

    [MethodImpl(INLINE)]
    public static void ChangeCapacity<T>(ref T[] array, int newSize)
    {
        if (newSize < array.Length)
            Array.Resize(ref array, Math.Max(0, newSize));
        else
        {
            uint newCapactiy = ((uint)newSize) * 2u;
            if (newCapactiy > int.MaxValue)
                newCapactiy = int.MaxValue;
            Array.Resize(ref array, (int)newCapactiy);
        }
    }
    #endregion

    #region ARRAY 2D
    public enum Anchor1D
    {
        Left,
        Center,
        Right,
    }

    public enum Anchor2D
    {
        TopLeft,
        Top,
        TopRight,
        Right,
        BottomRight,
        Bottom,
        BottomLeft,
        Left,
        Center,
    }

    public static T[] Expand<T>(this T[] array, int value, Anchor1D anchor = Anchor1D.Left)
    {
        if (value == 0)
            return array;

        bool isPositive = value > 0;
        var temp = new T[array.Length + value];
        value = Math.Abs(value);
        if (anchor == Anchor1D.Left)
        {
            Array.Copy(array, temp, array.Length - value);
        }
        else if (anchor == Anchor1D.Center)
        {
            if (isPositive)
                Array.Copy(array, 0, temp, value / 2, array.Length);
            else
                Array.Copy(array, value / 2, temp, 0, array.Length - value);
        }
        else if (anchor == Anchor1D.Right)
        {
            if (isPositive)
                Array.Copy(array, 0, temp, value, array.Length);
            else
                Array.Copy(array, value, temp, 0, array.Length - value);
        }

        return temp;
    }

    public static T[,] Expand<T>(this T[,] array, int gr, int gc, Anchor2D anchor = Anchor2D.TopLeft)
    {
        bool positiveR = gr >= 0;
        bool positiveC = gc >= 0;

        array.RowsColumns(out int rows, out int cols);
        int newRows = rows + gr;
        int newCols = cols + gc;

        T[,] newArr = new T[newRows, newCols];

        gr = Math.Abs(gr);
        gc = Math.Abs(gc);

        int halfgr = gr / 2;
        int halfgc = gc / 2;

        int rmin = Math.Min(rows, newRows);
        int cmin = Math.Min(cols, newCols);

        switch (anchor)
        {
            case Anchor2D.TopLeft:
            for (int r = 0; r < rmin; r++)
                for (int c = 0; c < cmin; c++)
                    newArr[r, c] = array[r, c];
            break;

            case Anchor2D.Top:
            if (positiveC)
            {
                for (int r = 0; r < rmin; r++)
                    for (int c = 0; c < cmin; c++)
                        newArr[r, c + halfgc] = array[r, c];
            }
            else
            {
                for (int r = 0; r < rmin; r++)
                    for (int c = 0; c < cmin; c++)
                        newArr[r, c] = array[r, c + halfgc];
            }
            break;

            case Anchor2D.TopRight:
            if (positiveC)
            {
                for (int r = 0; r < rmin; r++)
                    for (int c = 0; c < cmin; c++)
                        newArr[r, c + gc] = array[r, c];
            }
            else
            {
                for (int r = 0; r < rmin; r++)
                    for (int c = 0; c < cmin; c++)
                        newArr[r, c] = array[r, c + gc];
            }
            break;
            case Anchor2D.Right:
            if (positiveR && positiveC)
            {
                for (int r = 0; r < rmin; r++)
                    for (int c = 0; c < cmin; c++)
                        newArr[r + halfgr, c + gc] = array[r, c];
            }
            else if (positiveR && !positiveC)
            {
                for (int r = 0; r < rmin; r++)
                    for (int c = 0; c < cmin; c++)
                        newArr[r + halfgr, c] = array[r, c + gc];
            }
            else if (!positiveR && positiveC)
            {
                for (int r = 0; r < rmin; r++)
                    for (int c = 0; c < cmin; c++)
                        newArr[r, c + gc] = array[r + halfgr, c];
            }
            else //!!
            {
                for (int r = 0; r < rmin; r++)
                    for (int c = 0; c < cmin; c++)
                        newArr[r, c] = array[r + halfgr, c + gc];
            }
            break;

            case Anchor2D.BottomRight:
            if (positiveR && positiveC)
            {
                for (int r = 0; r < rmin; r++)
                    for (int c = 0; c < cmin; c++)
                        newArr[r + gr, c + gc] = array[r, c];
            }
            else if (positiveR && !positiveC)
            {
                for (int r = 0; r < rmin; r++)
                    for (int c = 0; c < cmin; c++)
                        newArr[r + gr, c] = array[r, c + gc];
            }
            else if (!positiveR && positiveC)
            {
                for (int r = 0; r < rmin; r++)
                    for (int c = 0; c < cmin; c++)
                        newArr[r, c + gc] = array[r + gr, c];
            }
            else //!!
            {
                for (int r = 0; r < rmin; r++)
                    for (int c = 0; c < cmin; c++)
                        newArr[r, c] = array[r + gr, c + gc];
            }
            break;

            case Anchor2D.Bottom:
            if (positiveR && positiveC)
            {
                for (int r = 0; r < rmin; r++)
                    for (int c = 0; c < cmin; c++)
                        newArr[r + gr, c + halfgc] = array[r, c];
            }
            else if (positiveR && !positiveC)
            {
                for (int r = 0; r < rmin; r++)
                    for (int c = 0; c < cmin; c++)
                        newArr[r + gr, c] = array[r, c + halfgc];
            }
            else if (!positiveR && positiveC)
            {
                for (int r = 0; r < rmin; r++)
                    for (int c = 0; c < cmin; c++)
                        newArr[r, c + halfgc] = array[r + gr, c];
            }
            else //!!
            {
                for (int r = 0; r < rmin; r++)
                    for (int c = 0; c < cmin; c++)
                        newArr[r, c] = array[r + gr, c + halfgc];
            }
            break;

            case Anchor2D.BottomLeft:
            if (positiveR)
            {
                for (int r = 0; r < rmin; r++)
                    for (int c = 0; c < cmin; c++)
                        newArr[r + gr, c] = array[r, c];
            }
            else
            {
                for (int r = 0; r < rmin; r++)
                    for (int c = 0; c < cmin; c++)
                        newArr[r, c] = array[r + gr, c];
            }
            break;

            case Anchor2D.Left:
            if (positiveR)
            {
                for (int r = 0; r < rmin; r++)
                    for (int c = 0; c < cmin; c++)
                        newArr[r + halfgr, c] = array[r, c];
            }
            else
            {
                for (int r = 0; r < rmin; r++)
                    for (int c = 0; c < cmin; c++)
                        newArr[r, c] = array[r + halfgr, c];
            }
            break;

            case Anchor2D.Center:
            if (positiveR && positiveC)
            {
                for (int r = 0; r < rmin; r++)
                    for (int c = 0; c < cmin; c++)
                        newArr[r + halfgr, c + halfgc] = array[r, c];
            }
            else if (positiveR && !positiveC)
            {
                for (int r = 0; r < rmin; r++)
                    for (int c = 0; c < cmin; c++)
                        newArr[r + halfgr, c] = array[r, c + halfgc];
            }
            else if (!positiveR && positiveC)
            {
                for (int r = 0; r < rmin; r++)
                    for (int c = 0; c < cmin; c++)
                        newArr[r, c + halfgc] = array[r + halfgr, c];
            }
            else //!!
            {
                for (int r = 0; r < rmin; r++)
                    for (int c = 0; c < cmin; c++)
                        newArr[r, c] = array[r + halfgr, c + halfgc];
            }
            break;
        }
        return newArr;
    }

    [MethodImpl(INLINE)] public static int Rows<T>(this T[,] array) => array.GetLength(0);
    [MethodImpl(INLINE)] public static int Columns<T>(this T[,] array) => array.GetLength(1);

    [MethodImpl(INLINE)]
    public static void RowsColumns<T>(this T[,] array, out int rows, out int collums)
    {
        rows = array.GetLength(0);
        collums = array.GetLength(1);
    }

    public static IEnumerable<T> RowMajor<T>(this T[,] array)
    {
        RowsColumns(array, out int rows, out int cols);
        for (int r = 0; r < rows; r++)
            for (int c = 0; c < cols; c++)
                yield return array[r, c];
    }
    public static IEnumerable<T> ColumnMajor<T>(this T[,] array)
    {
        RowsColumns(array, out int rows, out int cols);
        for (int c = 0; c < cols; c++)
            for (int r = 0; r < rows; r++)
                yield return array[r, c];
    }

    #endregion

    #region Swap
    [MethodImpl(INLINE)]
    public static void Swap<T>(this T[] array, int indexA, int indexB) =>
        (array[indexB], array[indexA]) = (array[indexA], array[indexB]);
    [MethodImpl(INLINE)]
    public static void Swap<T>(this List<T> list, int indexA, int indexB) =>
        (list[indexB], list[indexA]) = (list[indexA], list[indexB]);
    [MethodImpl(INLINE)]
    public static void Swap<T, K>(this IDictionary<T, K> dict, T indexA, T indexB) =>
        (dict[indexB], dict[indexA]) = (dict[indexA], dict[indexB]);
    #endregion

    #region MinMax
    public static void MinMaxValue(IList<int> list, out int min, out int max)
    {
        min = list[0];
        max = list[0];
        for (int i = list.Count - 1; i > 0; i--)
        {
            var item = list[i];
            if (min > item)
                min = item;
            else if (max < item)
                max = item;
        }
    }
    public static void MinMaxValue(IList<float> list, out float min, out float max)
    {
        min = list[0];
        max = list[0];
        for (int i = list.Count - 1; i > 0; i--)
        {
            var item = list[i];
            if (min > item)
                min = item;
            else if (max < item)
                max = item;
        }
    }
    #endregion

    #region List

    [MethodImpl(INLINE)]
    public static void SortReverse<T>(this List<T> list) where T : IComparable<T> => list.Sort(Classes.ComparerReverse<T>.Default);

    [MethodImpl(INLINE)]
    public static int AddSorted<T>(this List<T> list, T item) where T : IComparable<T>
    {
        var index = list.BinarySearch(item);
        if (index < 0)
            index = ~index;
        list.Insert(index, item);
        return index;
    }
    [MethodImpl(INLINE)]
    public static int AddSorted<T>(this List<T> list, T item, IComparer<T> comparer)
    {
        var index = list.BinarySearch(item, comparer);
        if (index < 0)
            index = ~index;
        list.Insert(index, item);
        return index;
    }

    [MethodImpl(INLINE)]
    public static bool TryAdd<T>(this List<T> list, T item)
    {
        int index = list.IndexOf(item);
        if (index == -1)
        {
            list.Add(item);
            return true;
        }
        return false;
    }
    #endregion

    #region Dict

    [MethodImpl(INLINE)]
    public static TValue? GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) =>
        dictionary.TryGetValue(key, out var value) ? value : default;

    public static TValue? TryRemoveValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
    {
        if (dictionary.TryGetValue(key, out var value))
        {
            dictionary.Remove(key);
            return value;
        }
        return default;
    }

    public static Dictionary<TKey, TValue> DictionaryFromCollection<TKey, TValue>(ICollection<TKey> keys, Func<TKey, TValue> newFunc) where TKey : notnull
    {
        var dict = new Dictionary<TKey, TValue>(keys.Count);
        foreach (var key in keys)
            dict.Add(key, newFunc(key));
        return dict;
    }

    #endregion

}
