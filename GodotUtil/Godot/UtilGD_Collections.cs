using System.Diagnostics.CodeAnalysis;

namespace Godot;

public static partial class UtilGD
{

    #region Collections Array

    public static object[] GDArrayToArray(GDC.Array gdArray)
    {
        var len = gdArray.Count;
        var arr = new object[len];
        for (int i = 0; i < len; i++)
            arr[i] = gdArray[i];
        return arr;
    }
    public static T[] GDArrayToArray<[MustBeVariant] T>(GDC.Array gdArray)
    {
        var len = gdArray.Count;
        var arr = new T[len];
        for (int i = 0; i < len; i++)
            arr[i] = gdArray[i].As<T>();
        return arr;
    }
    public static T[] GDArrayToArray<[MustBeVariant] T>(GDC.Array<T> gdArray)
    {
        var len = gdArray.Count;
        var arr = new T[len];
        for (int i = 0; i < len; i++)
            arr[i] = gdArray[i];
        return arr;
    }

    #endregion

    #region Collections List

    public static List<object> GDArrayToList(GDC.Array gdArray)
    {
        var len = gdArray.Count;
        var list = new List<object>(len);
        for (int i = 0; i < len; i++)
            list[i] = gdArray[i];
        return list;
    }
    public static List<T> GDArrayToList<[MustBeVariant] T>(GDC.Array gdArray)
    {
        var len = gdArray.Count;
        var list = new List<T>(len);
        for (int i = 0; i < len; i++)
            list[i] = gdArray[i].As<T>();
        return list;
    }
    public static List<T> GDArrayToList<[MustBeVariant] T>(GDC.Array<T> gdArray)
    {
        var len = gdArray.Count;
        var list = new List<T>(len);
        for (int i = 0; i < len; i++)
            list[i] = gdArray[i];
        return list;
    }

    #endregion

    #region Collections Dictionary

    public static bool TryGetValue<[MustBeVariant]T>(this GDC.Dictionary gdDict, Variant key, [MaybeNullWhen(false)] out T value)
    {
        if (gdDict.TryGetValue(key, out var variant))
        {
            value = variant.As<T>();
            return true;
        }
        value = default;
        return false;
    }

    public static Dictionary<Variant, Variant> GDDictToDict(GDC.Dictionary gdDict)
    {
        var len = gdDict.Count;
        var dic = new Dictionary<Variant, Variant>(len);
        foreach (var item in gdDict)
            dic.Add(item.Key, item.Value);
        return dic;
    }
    public static Dictionary<K, V> GDDictToDict<[MustBeVariant] K, [MustBeVariant] V>(GDC.Dictionary gdDict) where K : notnull
    {
        var len = gdDict.Count;
        var dic = new Dictionary<K, V>(len);
        foreach (var item in gdDict)
            dic.Add(item.Key.As<K>(), item.Value.As<V>());
        return dic;
    }
    public static Dictionary<K, V> GDDictToDict<[MustBeVariant] K, [MustBeVariant] V>(GDC.Dictionary<K, V> gdDict) where K : notnull
    {
        var len = gdDict.Count;
        var dic = new Dictionary<K, V>(len);
        foreach (KeyValuePair<K, V> item in gdDict)
            dic.Add(item.Key, item.Value);
        return dic;
    }

    #endregion

}
