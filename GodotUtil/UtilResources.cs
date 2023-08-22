using Godot;
using Util;
using IO = System.IO;

namespace GodotUtil;

public static class UtilResources
{
    public static string GetResourceDir(this Resource res) => res.ResourcePath.GetBaseDir();

    public static string GetResourceFileName(this Resource res) => IO.Path.GetFileNameWithoutExtension(res.ResourcePath.AsSpan()).ToString();

    public static string GetResourceExtension(this Resource res) => IO.Path.GetExtension(res.ResourcePath.AsSpan()).ToString();

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