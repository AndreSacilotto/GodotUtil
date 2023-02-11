using System.Runtime.CompilerServices;
using Godot;

namespace Util;

public class GDException : Exception
{

	public GDException(GodotObject type, Error error, string message, [CallerMemberName] string member = "", [CallerLineNumber] int line = 0) :
		this(type.GetType(), error, message, member, line)
	{ }

	public GDException(Type type, Error error, string message, [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
		: base($"'{type.FullName}' called a {nameof(GDException)} with error '{error}' on line '{line}' of method '{member}': {message}") { }
}