
namespace Godot
{

	public class Marshallable<T> : Object
	{
		public T Value;
		public Marshallable() => Value = default;
		public Marshallable(T value) => Value = value;
	}

	public class MarshallableRef<T> : Reference
	{
		public T Value;
		public MarshallableRef() => Value = default;
		public MarshallableRef(T value) => Value = value;
	}

}
