
namespace Godot.Collections;

public partial class Marshallable<T> : GodotObject
{
	public T Value;
	public Marshallable() => Value = default!;
	public Marshallable(T value) => Value = value;
}

public partial class MarshallableRef<T> : RefCounted
{
	public T Value;
	public MarshallableRef() => Value = default!;
	public MarshallableRef(T value) => Value = value;
}
