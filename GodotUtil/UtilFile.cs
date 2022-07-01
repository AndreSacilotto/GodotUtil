using Godot;
using System;

namespace Util
{
    public static class UtilFile
    {
        #region I/O
        public static string ReadFile(string fullPath)
        {
            var file = new File();
            var er = file.Open(fullPath, File.ModeFlags.Read);
            if (er != Error.Ok)
                throw new GDException(typeof(UtilFile), "ReadFile", er);
            var str = file.GetAsText();
            file.Close();
            return str;
        }
        public static T ReadFile<T>(string fullPath, Func<File, T> action)
        {
            var file = new File();
            var er = file.Open(fullPath, File.ModeFlags.Read);
            if (er != Error.Ok)
                throw new GDException(typeof(UtilFile), "ReadFile", er);
            var result = action.Invoke(file);
            file.Close();
            return result;
        }

        public static void WriteFile(string fullPath, string data)
        {
            var file = new File();
            var er = file.Open(fullPath, File.ModeFlags.Write);
            if (er != Error.Ok)
                throw new GDException(typeof(UtilFile), "WriteFile", er);
            file.StoreString(data);
            file.Close();
        }
        public static void WriteFile(string fullPath, Action<File> action)
        {
            var file = new File();
            var er = file.Open(fullPath, File.ModeFlags.Write);
            if (er != Error.Ok)
                throw new GDException(typeof(UtilFile), "WriteFile", er);
            action.Invoke(file);
            file.Close();
        }

        #endregion

        public static string StandardizePath(string path) => path.Replace("\\\\", "/").Replace('\\', '/');

    }
}