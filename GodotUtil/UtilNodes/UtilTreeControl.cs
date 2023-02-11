using Godot;

namespace Util;

public static class UtilTreeControl
{
	public static IEnumerable<TreeItem> GetEnumerator(this TreeItem parent, bool deep = false)
	{
		var items = parent.GetChildren();
		foreach (var item in items)
		{
			yield return item;
			if (deep)
			{ 
				foreach (var item2 in GetEnumerator(item, deep))
					yield return item2;
			}
		}
	}

	public static TreeItem ClearAndRoot(this Tree tree)
	{
		tree.Clear();
		return tree.CreateItem();
	}

}