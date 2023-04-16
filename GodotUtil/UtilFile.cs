using Godot;

namespace Util;

public static class UtilFile
{
    public static string UnixPath(string path) => path.Replace(@"\\", "/").Replace('\\', '/');

    public static string ReadFile(string fullPath)
    {
        using var file = FileAccess.Open(fullPath, FileAccess.ModeFlags.Read);
        if (file == null)
        {
            var error = FileAccess.GetOpenError();
            if (error != Error.Ok)
                throw new GDException(typeof(UtilFile), error, nameof(ReadFile));
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
                throw new GDException(typeof(UtilFile), error, nameof(ReadFile));
            return default!;
        }
        else
            return action(file);
    }

    public static void WriteFile(string fullPath, string data)
    {
        using var file = FileAccess.Open(fullPath, FileAccess.ModeFlags.Read);
        if (file == null)
        {
            var error = FileAccess.GetOpenError();
            if (error != Error.Ok)
                throw new GDException(typeof(UtilFile), error, nameof(WriteFile));
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
                throw new GDException(typeof(UtilFile), error, nameof(WriteFile));
        }
        else
            action(file);
    }




}