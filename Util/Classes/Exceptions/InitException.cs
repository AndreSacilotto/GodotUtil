using System;
using System.Runtime.InteropServices;

namespace Util
{
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_Exception))]
	[ComVisible(true)]
	public class InitException : Exception
	{
		public InitException(Type type)
			: base($"The class of type: '{type.FullName}' was already called init") { }
	}

}
