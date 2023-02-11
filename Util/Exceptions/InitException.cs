namespace Util;

public class InitException : Exception
{
	public InitException(Type type)
		: base($"The class of type: '{type.FullName}' was already called init") { }
}
