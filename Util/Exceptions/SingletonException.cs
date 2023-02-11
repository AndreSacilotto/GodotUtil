namespace Util.Singleton;

public class SingletonException : Exception
{
	//public SingletonException() { }
	//public SingletonException(string message) : base(message) { }
	public SingletonException(Type type) : base($"One Instance of {type.FullName} already exists") { }

	//public SingletonException(string message, Exception inner) : base(message, inner) { }
	//protected SingletonException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
