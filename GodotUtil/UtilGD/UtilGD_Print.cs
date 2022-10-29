using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Godot
{
	public static partial class UtilGD
	{
		public static bool IsNullPrint(object value)
		{
			var isnull = value == null;
			if (isnull)
				GD.Print("null");
			return isnull;
		}

		[Conditional("DEBUG")]
		public static void PrintCollection<TK, TV>(IEnumerable<KeyValuePair<TK, TV>> ie, string separator = " ")
		{
			string str = "";
			foreach (var item in ie)
				str += $"({item.Key}, {item.Value}){separator}";
			GD.Print(str);
		}

		[Conditional("DEBUG")]
		public static void PrintCollection<T>(IEnumerable<T> ie, string separator = " ")
		{
			string str = "";
			foreach (var item in ie)
				str += item.ToString() + separator;
			str.Remove(str.Length - separator.Length);
			GD.Print(str);
		}

		[Conditional("DEBUG")]
		public static void PrintTree(SceneTree tree, bool pretty = false)
		{
			if (pretty)
				tree.Root.PrintTree();
			else
				tree.Root.PrintTreePretty();
		}

		[Conditional("DEBUG")]
		public static void PrintAssert(bool assertion, string message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
		{
			if (assertion)
				return;
			GD.PrintErr($"Assertion failed: {message} at {file}:{line}");
			var stackTrace = new StackTrace();
			GD.PrintErr(stackTrace.ToString());
			throw new ApplicationException($"Assertion failed: {message}");
		}

		[Conditional("DEBUG")]
		public static void PrintTrace(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
		{
			GD.Print($"Called from {memberName}\tMessage: {message}\tFile: {filePath}\tLine: {lineNumber}");
		}

	}
}
