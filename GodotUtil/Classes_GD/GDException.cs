using System;

namespace Godot
{
	public class GDException : Exception
	{
		public GDException(Type type, string message) : base($"{type.Name}->{message}") { }
		public GDException(Type type, string message, Error error) : base($"{type.Name}->{message}:{error}") { }

		public GDException(Object obj, string message) : this(obj.GetType(), message) { }
		public GDException(Object obj, string message, Error error) : this(obj.GetType(), message, error) { }
	}

}
