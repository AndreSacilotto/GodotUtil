using Godot;

namespace Util;

public static class UtilFile
{
	public static string StandardizePath(string path) => path.Replace("\\\\", "/").Replace('\\', '/');

	public static string ReadFile(string fullPath)
	{
		using var file = FileAccess.Open(fullPath, FileAccess.ModeFlags.Read);
		var error = FileAccess.GetOpenError();
		if (error != Error.Ok)
			throw new GDException(typeof(UtilFile), error, nameof(ReadFile));
		var str = file.GetAsText();
		return str;
	}

	public static T ReadFile<T>(string fullPath, Func<FileAccess, T> action)
	{
		using var file = FileAccess.Open(fullPath, FileAccess.ModeFlags.Read);
		var error = FileAccess.GetOpenError();
		if (error != Error.Ok)
			throw new GDException(typeof(UtilFile), error, nameof(ReadFile));
		var result = action(file);
		return result;
	}

	public static void WriteFile(string fullPath, string data)
	{
		using var file = FileAccess.Open(fullPath, FileAccess.ModeFlags.Read);
		var error = FileAccess.GetOpenError();
		if (error != Error.Ok)
			throw new GDException(typeof(UtilFile), error, nameof(WriteFile));
		file.StoreString(data);
	}

	public static void WriteFile(string fullPath, Action<FileAccess> action)
	{
		using var file = FileAccess.Open(fullPath, FileAccess.ModeFlags.Write);
		var error = FileAccess.GetOpenError();
		if (error != Error.Ok)
			throw new GDException(typeof(UtilFile), error, nameof(WriteFile));
		action(file);
	}

}