using Godot;

namespace GodotUtil;

public static class UtilFileSystem
{
    public static string UnixPath(string path) => path.Replace(@"\\", "/").Replace('\\', '/');

    #region Read
    public static string ReadFile(string fullPath)
    {
        using var file = FileAccess.Open(fullPath, FileAccess.ModeFlags.Read);
        if (file == null)
        {
            var error = FileAccess.GetOpenError();
            if (error != Error.Ok)
                throw new GDException(typeof(UtilFileSystem), error, nameof(ReadFile));
            return default!;
        }
        else
            return file.GetAsText();
    }

    public static T ReadFile<T>(string fullPath, Func<FileAccess, T> action)
    {
        using var file = FileAccess.Open(fullPath, FileAccess.ModeFlags.Read);
        if (file == null)
        {
            var error = FileAccess.GetOpenError();
            if (error != Error.Ok)
                throw new GDException(typeof(UtilFileSystem), error, nameof(ReadFile));
            return default!;
        }
        else
            return action(file);
    }
    #endregion

    #region Write

    public static void WriteFile(string fullPath, string data)
    {
        using var file = FileAccess.Open(fullPath, FileAccess.ModeFlags.Read);
        if (file == null)
        {
            var error = FileAccess.GetOpenError();
            if (error != Error.Ok)
                throw new GDException(typeof(UtilFileSystem), error, nameof(WriteFile));
        }
        else
            file.StoreString(data);
    }

    public static void WriteFile(string fullPath, Action<FileAccess> action)
    {
        using var file = FileAccess.Open(fullPath, FileAccess.ModeFlags.Write);
        if (file == null)
        {
            var error = FileAccess.GetOpenError();
            if (error != Error.Ok)
                throw new GDException(typeof(UtilFileSystem), error, nameof(WriteFile));
        }
        else
            action(file);
    }
    #endregion


    #region Folders

    public static string[] GetFilesAt(string path)
    {
        if (path[path.Length - 1] != '/')
            throw new Exception("Invalid Path");
        var arr = DirAccess.GetFilesAt(path);
        for (int i = 0; i < arr.Length; i++)
            arr[i] = path + arr[i];
        return arr;
    }

    public static IEnumerable<string[]> GetFilesAt(string path, int depth)
    {
        if (path[path.Length - 1] != '/')
            throw new Exception("Invalid Path");

        yield return DirAccess.GetFilesAt(path);
        if(depth > 0)
        {
            var folders = DirAccess.GetDirectoriesAt(path);
            foreach (var f in folders)
                foreach (var item in GetFilesAt(path + f + '/', depth - 1))
                    yield return item;
        }
    }

    #endregion

}