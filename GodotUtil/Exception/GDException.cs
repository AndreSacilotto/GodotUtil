using Godot;
using System.Runtime.CompilerServices;

namespace GodotUtil;

public class GDException : Exception
{
    public GDException(Type type, Error error, string message, [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
        : base($"'{type.FullName}' called a {nameof(GDException)} with error '{error}' on line '{line}' of method '{member}': {message}") { }

    public GDException(GodotObject type, Error error, string message, [CallerMemberName] string member = "", [CallerLineNumber] int line = 0) :
        this(type.GetType(), error, message, member, line)
    { }
}