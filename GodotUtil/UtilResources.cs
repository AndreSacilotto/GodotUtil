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


        #region Sharp Util

        public static Type GetCSharpScriptType(this CSharpScript sharpScript)
        {
            return Util.UtilReflection.GetType(GetResourceFileName(sharpScript));
        }

        #endregion
    }

}