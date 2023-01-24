using System;
using System.Collections.Generic;
using Godot;
using io = System.IO;

namespace Util
{
	public static class UtilResources
	{
		public static string GetResourceFileName(this Resource res) =>
			io.Path.GetFileNameWithoutExtension(res.ResourcePath);

		public static string GetResourceDir(this Resource res) =>
			res.ResourcePath.GetBaseDir();

		public static string GetResourceExtension(this Resource res) =>
			io.Path.GetExtension(res.ResourcePath);

		public static List<Resource> GetResourcesInPath(string path)
		{
			var list = new List<Resource>();
			var dir = new Directory();
			dir.Open(path);
			dir.ListDirBegin();
			string filePath = dir.GetNext();
			while (filePath != string.Empty)
			{
				//list.Add(GD.Load(filePath));
				list.Add(ResourceLoader.Load(filePath));
				filePath = dir.GetNext();
			}
			dir.ListDirEnd();
			return list;
		}


		#region CSharpScript
		// CSharpScript methods only works if it inherit from Godot.Object


		public static Type GetCSharpScriptType(this CSharpScript script, bool reflection = false)
		{
			if (reflection)
				return UtilReflection.GetType(GetResourceFileName(script)) ?? throw new Exception("Cant find script type");
			else
				return Type.GetType(script.GetInstanceBaseType());
		}

		public static T New<T>(this CSharpScript script) where T : Godot.Object
		{
			return (T)script.New();
		}

		#endregion
	}

}