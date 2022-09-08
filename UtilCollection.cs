using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Util
{
    public static class UtilCollection
    {

        #region Array

        public static T[] NewOfSameSize<T>(this T[] array) => new T[array.Length];
        public static T[,] NewOfSameSize<T>(this T[,] array) => new T[array.GetLength(0), array.GetLength(1)];
        public static T[] Clone<T>(this T[] array) => (T[])array.Clone();
        public static T[,] Clone<T>(this T[,] array) => (T[,])array.Clone();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        public static bool ArrayOfSameSize(this object[][] arrays)
        {
            int size = arrays[0].Length;
            for (int i = 1; i < arrays.Length; i++)
                if (size == arrays[i].Length)
                    return false;
            return true;
        }

        #endregion


        #region Row and Collums
        public static int Rows<T>(this T[,] array) => array.GetLength(0);
        public static int Columns<T>(this T[,] array) => array.GetLength(1);
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Swap<T>(this T[] array, int indexA, int indexB)
        {
            var temp = array[indexA];
            array[indexA] = array[indexB];
            array[indexB] = temp;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Swap<T, K>(this IList<T> list, int indexA, int indexB)
        {
            var temp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = temp;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Swap<T, K>(this IDictionary<T, K> dict, T indexA, T indexB)
        {
            var temp = dict[indexA];
            dict[indexA] = dict[indexB];
            dict[indexB] = temp;
        }
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

        #region List / Dict
        public static void AddSorted<T>(List<T> list, T item) where T : IComparable<T>
        {
            var index = list.BinarySearch(item);
            if (index < 0)
                index = ~index;
            list.Insert(index, item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) =>
            dictionary.TryGetValue(key, out var value) ? value : default;

        public static TValue TryRemoveValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            if (dictionary.TryGetValue(key, out var value)) 
            {
                dictionary.Remove(key);
                return value;
            }
            return default;
        }

        #endregion



    }
}
