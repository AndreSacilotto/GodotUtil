using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Godot
{
    public static partial class UtilGD
    {

        #region Collections Array

        public static object[] GDArray2Array(Collections.Array gdArray)
        {
            var len = gdArray.Count;
            var arr = new object[len];
            for (int i = 0; i < len; i++)
                arr[i] = gdArray[i];
            return arr;
        }
        public static T[] GDArray2Array<T>(Collections.Array gdArray)
        {
            var len = gdArray.Count;
            var arr = new T[len];
            for (int i = 0; i < len; i++)
                arr[i] = (T)gdArray[i];
            return arr;
        }
        public static T[] GDArray2Array<T>(Collections.Array<T> gdArray)
        {
            var len = gdArray.Count;
            var arr = new T[len];
            for (int i = 0; i < len; i++)
                arr[i] = gdArray[i];
            return arr;
        }

        public static List<object> GDArray2List(Collections.Array gdArray)
        {
            var len = gdArray.Count;
            var list = new List<object>(len);
            for (int i = 0; i < len; i++)
                list[i] = gdArray[i];
            return list;
        }
        public static List<T> GDArray2List<T>(Collections.Array gdArray)
        {
            var len = gdArray.Count;
            var list = new List<T>(len);
            for (int i = 0; i < len; i++)
                list[i] = (T)gdArray[i];
            return list;
        }
        public static List<T> GDArray2List<T>(Collections.Array<T> gdArray)
        {
            var len = gdArray.Count;
            var list = new List<T>(len);
            for (int i = 0; i < len; i++)
                list[i] = gdArray[i];
            return list;
        }

        #endregion

        #region Collections Dictionary

        public static Dictionary<object, object> GDDict2Dict(Collections.Dictionary gdDict)
        {
            var len = gdDict.Count;
            var dic = new Dictionary<object, object>(len);
            foreach (KeyValuePair<object, object> item in gdDict)
                dic.Add(item.Key, item.Value);
            return dic;
        }
        public static Dictionary<K, V> GDDict2Dict<K, V>(Collections.Dictionary gdDict)
        {
            var len = gdDict.Count;
            var dic = new Dictionary<K, V>(len);
            foreach (KeyValuePair<K, V> item in gdDict)
                dic.Add(item.Key, item.Value);
            return dic;
        }

        public static Dictionary<K, V> GDDict2Dict<K, V>(Collections.Dictionary<K, V> gdDict)
        {
            var len = gdDict.Count;
            var dic = new Dictionary<K, V>(len);
            foreach (KeyValuePair<K, V> item in gdDict)
                dic.Add(item.Key, item.Value);
            return dic;
        }


        #endregion

    }
}
