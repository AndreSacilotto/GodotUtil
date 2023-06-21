using System.Runtime.CompilerServices;

namespace Util;

public class TypeException : Exception
{
    public TypeException(string message, Type type, [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
        : base($"'{type.FullName}' called a expection on line '{line}' of method '{member}': {message}") { }

    public TypeException(Type type, [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
        : base($"'{type.FullName}' called a expection on line '{line}' of method '{member}'") { }
}
