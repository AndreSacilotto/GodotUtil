using System;
using System.Reflection;
using System.Runtime.CompilerServices;

using Path = System.IO.Path;

namespace Godot
{
    public static class SceneInstance
    {
        private static readonly char[] dirSeparators = { '/', '\\' };
        private static readonly string godotRoot = GetGodotRoot();

        public static string GetFilePath<T>([CallerFilePath] string csPath = "") where T : Node
        {
            const string EXTENSION = ".cs";

            if (!csPath.EndsWith(EXTENSION))
                throw new Exception($"Caller '{csPath}' is not cs file.");
            if (!csPath.StartsWith(godotRoot))
                throw new Exception($"Caller '{csPath}' is outside '{godotRoot}'.");

            string resolvePath = csPath.Substring(godotRoot.Length);
            resolvePath = resolvePath
                .Substring(0, resolvePath.Length - EXTENSION.Length)
                .TrimStart('/', '\\')
                .Replace("\\", "/");

            return resolvePath;
        }

        public static T Instanciate<T>(string path) where T : Node
        {
            var packed = GD.Load<PackedScene>($"res://{path}.tscn");
            var instance = packed.Instance();
            if (instance is T instanceT)
                return instanceT;
            throw new Exception($"The root of scene 'res://{path}.tscn' is not '{typeof(T)}'");
        }

        private static string GetGodotRoot([CallerFilePath] string rootResourcePath = "")
        {
            return rootResourcePath.Substring(0, rootResourcePath.LastIndexOfAny(dirSeparators));
        }

        // GD 4.0
        //public static T Instantiate<T>() where T : class
        //{
        //	var type = typeof(T);
        //	var attr = type.GetCustomAttribute<ScriptPathAttribute>();
        //	if (attr == null)
        //		throw new InvalidOperationException($"Type '{type}' does not have a ScriptPathAttribute.");

        //	var path = Path.ChangeExtension(attr.Path, ".tscn");
        //	var scene = GD.Load<PackedScene>(path);
        //	return scene.Instantiate<T>();
        //}

    }
}