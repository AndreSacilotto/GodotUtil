using Godot;
using Util;
using IO = System.IO;

namespace GodotUtil;

public static class UtilResources
{
    public static string GetResourceFileName(this Resource res) => IO.Path.GetFileNameWithoutExtension(res.ResourcePath);

    public static string GetResourceDir(this Resource res) => res.ResourcePath.GetBaseDir();

    public static string GetResourceExtension(this Resource res) => IO.Path.GetExtension(res.ResourcePath);

    #region Load

    public static List<T> LoadAll<T>(string path, ResourceLoader.CacheMode cacheMode = ResourceLoader.CacheMode.Reuse) where T : Resource
    {
        var list = new List<T>();
        using var dir = DirAccess.Open(path);
        if (dir == null)
        {
            var error = FileAccess.GetOpenError();
            if (error != Error.Ok)
                throw new GDException(typeof(UtilResources), error, nameof(LoadAll));
        }
        else
        {
            dir.ListDirBegin();
            var filePath = dir.GetNext();
            while (filePath != string.Empty)
            {
                if (!dir.CurrentIsDir())
                    list.Add(ResourceLoader.Load<T>(filePath, null, cacheMode));
                filePath = dir.GetNext();
            }
            dir.ListDirEnd();
        }
        return list;
    }

    public static List<T> LoadAll<T>(string path, int maxDepth, ResourceLoader.CacheMode cacheMode = ResourceLoader.CacheMode.Reuse) where T : Resource
    {
        var list = new List<T>();
        LoadAllRecursive(list, path, maxDepth, cacheMode);
        return list;

        static void LoadAllRecursive(List<T> buffer, string path, int maxDepth, ResourceLoader.CacheMode cacheMode)
        {
            using var dir = DirAccess.Open(path);

            if (dir == null)
            {
                var error = DirAccess.GetOpenError();
                if (error != Error.Ok)
                    throw new GDException(typeof(UtilResources), error, nameof(LoadAll));
                return;
            }

            dir.ListDirBegin();
            var itemPath = dir.GetNext();
            while (itemPath != string.Empty)
            {
                if (dir.CurrentIsDir())
                {
                    if (maxDepth > 0)
                        LoadAllRecursive(buffer, itemPath, maxDepth - 1, cacheMode);
                }
                else
                    buffer.Add(ResourceLoader.Load<T>(itemPath, null, cacheMode));
                itemPath = dir.GetNext();
            }
            dir.ListDirEnd();
        }

    }

    #endregion


    #region CSharpScript
    // CSharpScript methods only works if it inherit from Godot.GodotObject

    public static Type GetCSharpScriptType(this CSharpScript script, bool reflection = false)
    {
        if (reflection)
            return UtilReflection.GetType(GetResourceFileName(script)) ?? throw new Exception("Cant find script type");
        else
            return Type.GetType(script.GetInstanceBaseType()) ?? throw new Exception("Cant find script type");
    }

    public static T New<T>(this CSharpScript script) where T : GodotObject => (T)script.New();

    #endregion
}