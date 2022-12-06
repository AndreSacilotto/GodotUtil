using System;
using System.Runtime.CompilerServices;
using static Util.UtilShared;
using number_t = System.Single;
using vector2_t = Godot.Vector2;
using vector3_t = Godot.Vector3;

namespace Godot
{

	public static partial class VectorExt
	{
		// ----------------------- REF -----------------------

		#region Setting

		[MethodImpl(INLINE)] public static void SetXRef(this ref vector2_t item, number_t x) => item.x = x;
		[MethodImpl(INLINE)] public static void SetYRef(this ref vector2_t item, number_t y) => item.y = y;

		[MethodImpl(INLINE)] public static void SetXRef(this ref vector3_t item, number_t x) => item.x = x;
		[MethodImpl(INLINE)] public static void SetYRef(this ref vector3_t item, number_t y) => item.y = y;
		[MethodImpl(INLINE)] public static void SetZRef(this ref vector3_t item, number_t z) => item.z = z;

		[MethodImpl(INLINE)] public static void NegXRef(this ref vector2_t item) => item.x = -item.x;
		[MethodImpl(INLINE)] public static void NegYRef(this ref vector2_t item) => item.y = -item.y;

		[MethodImpl(INLINE)] public static void NegXRef(this ref vector3_t item) => item.x = -item.x;
		[MethodImpl(INLINE)] public static void NegYRef(this ref vector3_t item) => item.x = -item.y;
		[MethodImpl(INLINE)] public static void NegZRef(this ref vector3_t item) => item.x = -item.z;

		#endregion

		#region Adding

		[MethodImpl(INLINE)]
		public static void AddXYRef(this ref vector2_t item, number_t value)
		{
			item.x += value;
			item.y += value;
		}
		[MethodImpl(INLINE)] public static void AddXRef(this ref vector2_t item, number_t x) => item.x += x;
		[MethodImpl(INLINE)] public static void AddYRef(this ref vector2_t item, number_t y) => item.y += y;

		[MethodImpl(INLINE)]
		public static void AddXYZRef(this ref vector3_t item, number_t value)
		{
			item.x += value;
			item.y += value;
			item.z += value;
		}
		[MethodImpl(INLINE)] public static void AddXRef(this ref vector3_t item, number_t x) => item.x += x;
		[MethodImpl(INLINE)] public static void AddYRef(this ref vector3_t item, number_t y) => item.y += y;
		[MethodImpl(INLINE)] public static void AddZRef(this ref vector3_t item, number_t z) => item.y += z;

		#endregion

		// ----------------------- NON REF -----------------------

		#region Setting

		[MethodImpl(INLINE)] public static vector2_t SetX(this vector2_t item, number_t x) => new(x, item.y);
		[MethodImpl(INLINE)] public static vector2_t SetY(this vector2_t item, number_t y) => new(item.x, y);

		[MethodImpl(INLINE)] public static vector3_t SetX(this vector3_t item, number_t x) => new(x, item.y, item.z);
		[MethodImpl(INLINE)] public static vector3_t SetY(this vector3_t item, number_t y) => new(item.x, y, item.z);
		[MethodImpl(INLINE)] public static vector3_t SetZ(this vector3_t item, number_t z) => new(item.x, item.y, z);

		[MethodImpl(INLINE)] public static vector2_t NegX(this vector2_t item) => new(-item.x, item.y);
		[MethodImpl(INLINE)] public static vector2_t NegY(this vector2_t item) => new(item.x, -item.y);

		[MethodImpl(INLINE)] public static vector3_t NegX(this vector3_t item) => new(-item.x, item.y, item.x);
		[MethodImpl(INLINE)] public static vector3_t NegY(this vector3_t item) => new(item.x, -item.y, item.y);
		[MethodImpl(INLINE)] public static vector3_t NegZ(this vector3_t item) => new(item.x, item.y, -item.z);

		#endregion

		#region Adding

		[MethodImpl(INLINE)] public static vector2_t AddXY(this vector2_t item, number_t value) => new(item.x + value, item.y + value);
		[MethodImpl(INLINE)] public static vector2_t AddX(this vector2_t item, number_t x) => new(item.x + x, item.y);
		[MethodImpl(INLINE)] public static vector2_t AddY(this vector2_t item, number_t y) => new(item.x, item.y + y);

		[MethodImpl(INLINE)] public static vector3_t AddXYZ(this vector3_t item, number_t value) => new(item.x + value, item.y + value, item.z + value);
		[MethodImpl(INLINE)] public static vector3_t AddX(this vector3_t item, number_t x) => new(item.x + x, item.y, item.z);
		[MethodImpl(INLINE)] public static vector3_t AddY(this vector3_t item, number_t y) => new(item.x, item.y + y, item.z);
		[MethodImpl(INLINE)] public static vector3_t AddZ(this vector3_t item, number_t z) => new(item.x, item.y, item.z + z);

		#endregion

		#region New

		[MethodImpl(INLINE)] public static vector2_t Copy(this vector2_t vec) => new(vec.x, vec.y);
		[MethodImpl(INLINE)] public static vector3_t Copy(this vector3_t vec) => new(vec.x, vec.y, vec.z);

		[MethodImpl(INLINE)] public static vector2_t CreateVec2(number_t value) => new(value, value);

		[MethodImpl(INLINE)] public static vector3_t CreateVec3(number_t value) => new(value, value, value);

		#endregion

		#region Clamping

		[MethodImpl(INLINE)] public static vector2_t Clamp(this vector2_t item, number_t min, number_t max) => new(Mathf.Clamp(item.x, min, max), Mathf.Clamp(item.y, min, max));
		[MethodImpl(INLINE)] public static vector2_t Clamp(this vector2_t item, vector2_t min, vector2_t max) => new(Mathf.Clamp(item.x, min.x, max.x), Mathf.Clamp(item.y, min.y, max.y));

		[MethodImpl(INLINE)] public static vector2_t Max(this vector2_t item, number_t max) => new(Math.Max(item.x, max), Math.Max(item.y, max));
		[MethodImpl(INLINE)] public static vector2_t Max(this vector2_t item, vector2_t max) => new(Math.Max(item.x, max.x), Math.Max(item.y, max.y));

		[MethodImpl(INLINE)] public static vector2_t Min(this vector2_t item, number_t min) => new(Math.Min(item.x, min), Math.Min(item.y, min));
		[MethodImpl(INLINE)] public static vector2_t Min(this vector2_t item, vector2_t min) => new(Math.Min(item.x, min.x), Math.Min(item.y, min.y));


		#endregion

	}

}
