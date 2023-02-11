using System.Runtime.CompilerServices;

using vector2_t = Godot.Vector2;
using vector3_t = Godot.Vector3;
#if REAL_T_IS_DOUBLE
using number_t = System.Double;
#else
using number_t = System.Single;
#endif

namespace Godot;


public static partial class VectorExt
{
	// ----------------------- REF -----------------------

	#region Setting

	[MethodImpl(INLINE)] public static void SetXRef(this ref vector2_t item, number_t x) => item.X = x;
	[MethodImpl(INLINE)] public static void SetYRef(this ref vector2_t item, number_t y) => item.Y = y;

	[MethodImpl(INLINE)] public static void SetXRef(this ref vector3_t item, number_t x) => item.X = x;
	[MethodImpl(INLINE)] public static void SetYRef(this ref vector3_t item, number_t y) => item.Y = y;
	[MethodImpl(INLINE)] public static void SetZRef(this ref vector3_t item, number_t z) => item.Z = z;

	[MethodImpl(INLINE)] public static void NegXRef(this ref vector2_t item) => item.X = -item.X;
	[MethodImpl(INLINE)] public static void NegYRef(this ref vector2_t item) => item.Y = -item.Y;

	[MethodImpl(INLINE)] public static void NegXRef(this ref vector3_t item) => item.X = -item.X;
	[MethodImpl(INLINE)] public static void NegYRef(this ref vector3_t item) => item.X = -item.Y;
	[MethodImpl(INLINE)] public static void NegZRef(this ref vector3_t item) => item.X = -item.Z;

	#endregion

	#region Adding

	[MethodImpl(INLINE)]
	public static void AddXYRef(this ref vector2_t item, number_t value)
	{
		item.X += value;
		item.Y += value;
	}
	[MethodImpl(INLINE)] public static void AddXRef(this ref vector2_t item, number_t x) => item.X += x;
	[MethodImpl(INLINE)] public static void AddYRef(this ref vector2_t item, number_t y) => item.Y += y;

	[MethodImpl(INLINE)]
	public static void AddXYZRef(this ref vector3_t item, number_t value)
	{
		item.X += value;
		item.Y += value;
		item.Z += value;
	}
	[MethodImpl(INLINE)] public static void AddXRef(this ref vector3_t item, number_t x) => item.X += x;
	[MethodImpl(INLINE)] public static void AddYRef(this ref vector3_t item, number_t y) => item.Y += y;
	[MethodImpl(INLINE)] public static void AddZRef(this ref vector3_t item, number_t z) => item.Y += z;

	#endregion

	// ----------------------- NON REF -----------------------

	#region Setting

	[MethodImpl(INLINE)] public static vector2_t SetX(this vector2_t item, number_t x) => new(x, item.Y);
	[MethodImpl(INLINE)] public static vector2_t SetY(this vector2_t item, number_t y) => new(item.X, y);

	[MethodImpl(INLINE)] public static vector3_t SetX(this vector3_t item, number_t x) => new(x, item.Y, item.Z);
	[MethodImpl(INLINE)] public static vector3_t SetY(this vector3_t item, number_t y) => new(item.X, y, item.Z);
	[MethodImpl(INLINE)] public static vector3_t SetZ(this vector3_t item, number_t z) => new(item.X, item.Y, z);

	[MethodImpl(INLINE)] public static vector2_t NegX(this vector2_t item) => new(-item.X, item.Y);
	[MethodImpl(INLINE)] public static vector2_t NegY(this vector2_t item) => new(item.X, -item.Y);

	[MethodImpl(INLINE)] public static vector3_t NegX(this vector3_t item) => new(-item.X, item.Y, item.X);
	[MethodImpl(INLINE)] public static vector3_t NegY(this vector3_t item) => new(item.X, -item.Y, item.Y);
	[MethodImpl(INLINE)] public static vector3_t NegZ(this vector3_t item) => new(item.X, item.Y, -item.Z);

	#endregion

	#region Adding

	[MethodImpl(INLINE)] public static vector2_t AddXY(this vector2_t item, number_t value) => new(item.X + value, item.Y + value);
	[MethodImpl(INLINE)] public static vector2_t AddX(this vector2_t item, number_t x) => new(item.X + x, item.Y);
	[MethodImpl(INLINE)] public static vector2_t AddY(this vector2_t item, number_t y) => new(item.X, item.Y + y);

	[MethodImpl(INLINE)] public static vector3_t AddXYZ(this vector3_t item, number_t value) => new(item.X + value, item.Y + value, item.Z + value);
	[MethodImpl(INLINE)] public static vector3_t AddX(this vector3_t item, number_t x) => new(item.X + x, item.Y, item.Z);
	[MethodImpl(INLINE)] public static vector3_t AddY(this vector3_t item, number_t y) => new(item.X, item.Y + y, item.Z);
	[MethodImpl(INLINE)] public static vector3_t AddZ(this vector3_t item, number_t z) => new(item.X, item.Y, item.Z + z);

	#endregion

	#region New

	[MethodImpl(INLINE)] public static vector2_t Copy(this vector2_t vec) => new(vec.X, vec.Y);
	[MethodImpl(INLINE)] public static vector3_t Copy(this vector3_t vec) => new(vec.X, vec.Y, vec.Z);

	[MethodImpl(INLINE)] public static vector2_t CreateVec2(number_t value) => new(value, value);

	[MethodImpl(INLINE)] public static vector3_t CreateVec3(number_t value) => new(value, value, value);

	#endregion

	#region Clamping

	[MethodImpl(INLINE)] public static vector2_t Clamp(this vector2_t item, number_t min, number_t max) => new(Mathf.Clamp(item.X, min, max), Mathf.Clamp(item.Y, min, max));
	[MethodImpl(INLINE)] public static vector2_t Clamp(this vector2_t item, vector2_t min, vector2_t max) => new(Mathf.Clamp(item.X, min.X, max.X), Mathf.Clamp(item.Y, min.Y, max.Y));

	[MethodImpl(INLINE)] public static vector2_t Max(this vector2_t item, number_t max) => new(Math.Max(item.X, max), Math.Max(item.Y, max));
	[MethodImpl(INLINE)] public static vector2_t Max(this vector2_t item, vector2_t max) => new(Math.Max(item.X, max.X), Math.Max(item.Y, max.Y));

	[MethodImpl(INLINE)] public static vector2_t Min(this vector2_t item, number_t min) => new(Math.Min(item.X, min), Math.Min(item.Y, min));
	[MethodImpl(INLINE)] public static vector2_t Min(this vector2_t item, vector2_t min) => new(Math.Min(item.X, min.X), Math.Min(item.Y, min.Y));


	#endregion

}
