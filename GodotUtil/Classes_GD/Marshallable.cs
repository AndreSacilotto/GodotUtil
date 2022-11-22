
namespace Godot
{

#if NETFRAMEWORK
	public class Marshallable<T> : Object
#else
	public partial class Marshallable<T> : Object
#endif
	{
		public T Value;
		public Marshallable() => Value = default;
		public Marshallable(T value) => Value = value;
	}

#if NETFRAMEWORK
	public class MarshallableRef<T> : Reference
#else
	public partial class MarshallableRef<T> : RefCounted
#endif
	{
		public T Value;
		public MarshallableRef() => Value = default;
		public MarshallableRef(T value) => Value = value;
	}

}
