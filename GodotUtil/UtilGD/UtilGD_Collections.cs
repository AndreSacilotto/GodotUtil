namespace Godot;

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
	public static T[] GDArrayToArray<[MustBeVariant] T>(Collections.Array gdArray)
	{
		var len = gdArray.Count;
		var arr = new T[len];
		for (int i = 0; i < len; i++)
			arr[i] = gdArray[i].As<T>();
		return arr;
	}
	public static T[] GDArrayToArray<[MustBeVariant] T>(Collections.Array<T> gdArray)
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
	public static List<T> GDArrayToList<[MustBeVariant] T>(Collections.Array gdArray)
	{
		var len = gdArray.Count;
		var list = new List<T>(len);
		for (int i = 0; i < len; i++)
			list[i] = gdArray[i].As<T>();
		return list;
	}
	public static List<T> GDArrayToList<[MustBeVariant] T>(Collections.Array<T> gdArray)
	{
		var len = gdArray.Count;
		var list = new List<T>(len);
		for (int i = 0; i < len; i++)
			list[i] = gdArray[i];
		return list;
	}

	#endregion

	#region Collections Dictionary

	public static Dictionary<Variant, Variant> GDDictToDict(Collections.Dictionary gdDict)
	{
		var len = gdDict.Count;
		var dic = new Dictionary<Variant, Variant>(len);
		foreach (var item in gdDict)
			dic.Add(item.Key, item.Value);
		return dic;
	}
	public static Dictionary<K, V> GDDictToDict<[MustBeVariant] K, [MustBeVariant] V>(Collections.Dictionary gdDict) where K : notnull
	{
		var len = gdDict.Count;
		var dic = new Dictionary<K, V>(len);
		foreach (var item in gdDict)
			dic.Add(item.Key.As<K>(), item.Value.As<V>());
		return dic;
	}
	public static Dictionary<K, V> GDDictToDict<[MustBeVariant] K, [MustBeVariant] V>(Collections.Dictionary<K, V> gdDict) where K : notnull
	{
		var len = gdDict.Count;
		var dic = new Dictionary<K, V>(len);
		foreach (KeyValuePair<K, V> item in gdDict)
			dic.Add(item.Key, item.Value);
		return dic;
	}

	#endregion

}
