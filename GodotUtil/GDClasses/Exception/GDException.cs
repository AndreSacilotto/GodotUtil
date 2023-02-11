using System;
using System.Runtime.InteropServices;

namespace Util
{
	public class GDException : Exception
	{
		public GDException(Type type, string message, Godot.Error error) : 
			base($"{type.Name} -> {message} with error {error}") { }		
		
		public GDException(Godot.Object type, string message, Godot.Error error) : 
			this(type.GetType(), message, error) { }
	}
}