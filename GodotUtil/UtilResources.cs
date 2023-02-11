using Godot;
using io = System.IO;

namespace Util;

public static class UtilResources
{
	public static string GetResourceFileName(this Resource res) =>
		io.Path.GetFileNameWithoutExtension(res.ResourcePath);

	public static string GetResourceDir(this Resource res) =>
		res.ResourcePath.GetBaseDir();

	public static string GetResourceExtension(this Resource res) =>
		io.Path.GetExtension(res.ResourcePath);

	public static List<Resource> GetResourcesInPath(string path, ResourceLoader.CacheMode cacheMode = ResourceLoader.CacheMode.Reuse)
	{
		var list = new List<Resource>();
		using var dir = DirAccess.Open(path);
		if (dir != null)
		{
			dir.ListDirBegin();
			var filePath = dir.GetNext();
			while (filePath != string.Empty)
			{
				list.Add(ResourceLoader.Load(filePath, null, cacheMode));
				filePath = dir.GetNext();
			}
			dir.ListDirEnd();
		}
		return list;
	}

	#region CSharpScript
	// CSharpScript methods only works if it inherit from Godot.GodotObject

	public static Type GetCSharpScriptType(this CSharpScript script, bool reflection = false)
	{
		if (reflection)
			return UtilReflection.GetType(GetResourceFileName(script)) ?? throw new Exception("Cant find script type");
		else
			return Type.GetType(script.GetInstanceBaseType()) ?? throw new Exception("Cant find script type");
	}

	public static T New<T>(this CSharpScript script) where T : GodotObject
	{
		return (T)script.New();
	}

	#endregion
}