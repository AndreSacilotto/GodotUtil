using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Godot
{
    public static partial class UtilGD
    {

        #region Collections Array

        public static object[] GDArrayToArray(Collections.Array gdArray)
        {
            var len = gdArray.Count;
            var arr = new object[len];
            for (int i = 0; i < len; i++)
                arr[i] = gdArray[i];
            return arr;
        }
        public static T[] GDArrayToArray<T>(Collections.Array gdArray)
        {
            var len = gdArray.Count;
            var arr = new T[len];
            for (int i = 0; i < len; i++)
                arr[i] = (T)gdArray[i];
            return arr;
        }
        public static T[] GDArrayToArray<T>(Collections.Array<T> gdArray)
        {
            var len = gdArray.Count;
            var arr = new T[len];
            for (int i = 0; i < len; i++)
                arr[i] = gdArray[i];
            return arr;
        }

        #endregion

        #region Collections List

        public static List<object> GDArrayToList(Collections.Array gdArray)
        {
            var len = gdArray.Count;
            var list = new List<object>(len);
            for (int i = 0; i < len; i++)
                list[i] = gdArray[i];
            return list;
        }
        public static List<T> GDArrayToList<T>(Collections.Array gdArray)
        {
            var len = gdArray.Count;
            var list = new List<T>(len);
            for (int i = 0; i < len; i++)
                list[i] = (T)gdArray[i];
            return list;
        }
        public static List<T> GDArrayToList<T>(Collections.Array<T> gdArray)
        {
            var len = gdArray.Count;
            var list = new List<T>(len);
            for (int i = 0; i < len; i++)
                list[i] = gdArray[i];
            return list;
        }

        #endregion

        #region Collections Dictionary

        public static Dictionary<object, object> GDDictToDict(Collections.Dictionary gdDict)
        {
            var len = gdDict.Count;
            var dic = new Dictionary<object, object>(len);
            foreach (KeyValuePair<object, object> item in gdDict)
                dic.Add(item.Key, item.Value);
            return dic;
        }
        public static Dictionary<K, V> GDDictToDict<K, V>(Collections.Dictionary gdDict)
        {
            var len = gdDict.Count;
            var dic = new Dictionary<K, V>(len);
            foreach (KeyValuePair<K, V> item in gdDict)
                dic.Add(item.Key, item.Value);
            return dic;
        }

        public static Dictionary<K, V> GDDictToDict<K, V>(Collections.Dictionary<K, V> gdDict)
        {
            var len = gdDict.Count;
            var dic = new Dictionary<K, V>(len);
            foreach (KeyValuePair<K, V> item in gdDict)
                dic.Add(item.Key, item.Value);
            return dic;
        }

        #endregion

        #region InputEvent

        public static void DisposeInputEventImmediately(this InputEvent inputEvent) 
        {
            inputEvent.Dispose();
            GC.Collect(GC.MaxGeneration);
            GC.WaitForPendingFinalizers();
        }

        #endregion

    }
}
