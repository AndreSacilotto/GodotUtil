using System;
using System.Runtime.InteropServices;

namespace Util.Singleton
{
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_Exception))]
	[ComVisible(true)]
	public class SingletonException : Exception
	{
		const string ERR_MESSAGE_TEXT = "One Instance of {0} already exists";

		public SingletonException() { }
		public SingletonException(string message) : base(message) { }
		public SingletonException(Type type) : base(string.Format(ERR_MESSAGE_TEXT, type.FullName)) { }

		//public SingletonException(string message, Exception inner) : base(message, inner) { }
		//protected SingletonException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
