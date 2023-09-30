using System.Runtime.CompilerServices;


#if REAL_T_IS_DOUBLE
using number_t = System.Double;
#else
using number_t = System.Single;
#endif
using vec2_t = Godot.Vector2;
using vec3_t = Godot.Vector3;

namespace Godot;

public static partial class VectorExt
{
    #region Setting

    [MethodImpl(INLINE)] public static vec2_t SetX(this vec2_t item, number_t x) => new(x, item.Y);
    [MethodImpl(INLINE)] public static vec2_t SetY(this vec2_t item, number_t y) => new(item.X, y);

    [MethodImpl(INLINE)] public static vec3_t SetX(this vec3_t item, number_t x) => new(x, item.Y, item.Z);
    [MethodImpl(INLINE)] public static vec3_t SetY(this vec3_t item, number_t y) => new(item.X, y, item.Z);
    [MethodImpl(INLINE)] public static vec3_t SetZ(this vec3_t item, number_t z) => new(item.X, item.Y, z);

    [MethodImpl(INLINE)] public static vec2_t NegX(this vec2_t item) => new(-item.X, item.Y);
    [MethodImpl(INLINE)] public static vec2_t NegY(this vec2_t item) => new(item.X, -item.Y);

    [MethodImpl(INLINE)] public static vec3_t NegX(this vec3_t item) => new(-item.X, item.Y, item.X);
    [MethodImpl(INLINE)] public static vec3_t NegY(this vec3_t item) => new(item.X, -item.Y, item.Y);
    [MethodImpl(INLINE)] public static vec3_t NegZ(this vec3_t item) => new(item.X, item.Y, -item.Z);

    #endregion

    #region Adding

    [MethodImpl(INLINE)] public static vec2_t AddX(this vec2_t item, number_t x) => new(item.X + x, item.Y);
    [MethodImpl(INLINE)] public static vec2_t AddY(this vec2_t item, number_t y) => new(item.X, item.Y + y);
    [MethodImpl(INLINE)] public static vec2_t AddXY(this vec2_t item, number_t value) => new(item.X + value, item.Y + value);

    [MethodImpl(INLINE)] public static vec3_t AddX(this vec3_t item, number_t x) => new(item.X + x, item.Y, item.Z);
    [MethodImpl(INLINE)] public static vec3_t AddY(this vec3_t item, number_t y) => new(item.X, item.Y + y, item.Z);
    [MethodImpl(INLINE)] public static vec3_t AddZ(this vec3_t item, number_t z) => new(item.X, item.Y, item.Z + z);
    [MethodImpl(INLINE)] public static vec3_t AddXY(this vec3_t item, number_t value) => new(item.X + value, item.Y + value, item.Z);
    [MethodImpl(INLINE)] public static vec3_t AddXZ(this vec3_t item, number_t value) => new(item.X + value, item.Y, item.Z + value);
    [MethodImpl(INLINE)] public static vec3_t AddYZ(this vec3_t item, number_t value) => new(item.X, item.Y + value, item.Z + value);
    [MethodImpl(INLINE)] public static vec3_t AddXYZ(this vec3_t item, number_t value) => new(item.X + value, item.Y + value, item.Z + value);

    #endregion

    #region New

    [MethodImpl(INLINE)] public static vec2_t Copy(this vec2_t vec) => new(vec.X, vec.Y);
    [MethodImpl(INLINE)] public static vec3_t Copy(this vec3_t vec) => new(vec.X, vec.Y, vec.Z);

    [MethodImpl(INLINE)] public static vec2_t CreateVec2(number_t value) => new(value, value);
    [MethodImpl(INLINE)] public static vec3_t CreateVec3(number_t value) => new(value, value, value);

    #endregion

    #region Clamping

    [MethodImpl(INLINE)] public static vec2_t Clamp(this vec2_t item, number_t min, number_t max) => new(Math.Clamp(item.X, min, max), Math.Clamp(item.Y, min, max));
    [MethodImpl(INLINE)] public static vec2_t Clamp(this vec2_t item, vec2_t min, vec2_t max) => new(Math.Clamp(item.X, min.X, max.X), Math.Clamp(item.Y, min.Y, max.Y));
    [MethodImpl(INLINE)] public static vec3_t Clamp(this vec3_t item, number_t min, number_t max) => new(Math.Clamp(item.X, min, max), Math.Clamp(item.Y, min, max), Math.Clamp(item.Z, min, max));
    [MethodImpl(INLINE)] public static vec3_t Clamp(this vec3_t item, vec3_t min, vec3_t max) => new(Math.Clamp(item.X, min.X, max.X), Math.Clamp(item.Y, min.Y, max.Y), Math.Clamp(item.Z, min.Z, max.Z));

    [MethodImpl(INLINE)] public static vec2_t Max(this vec2_t item, number_t max) => new(Math.Max(item.X, max), Math.Max(item.Y, max));
    [MethodImpl(INLINE)] public static vec2_t Max(this vec2_t item, vec2_t max) => new(Math.Max(item.X, max.X), Math.Max(item.Y, max.Y));
    [MethodImpl(INLINE)] public static vec3_t Max(this vec3_t item, number_t max) => new(Math.Max(item.X, max), Math.Max(item.Y, max), Math.Max(item.Z, max));
    [MethodImpl(INLINE)] public static vec3_t Max(this vec3_t item, vec3_t max) => new(Math.Max(item.X, max.X), Math.Max(item.Y, max.Y), Math.Max(item.Z, max.Z));

    [MethodImpl(INLINE)] public static vec2_t Min(this vec2_t item, number_t min) => new(Math.Min(item.X, min), Math.Min(item.Y, min));
    [MethodImpl(INLINE)] public static vec2_t Min(this vec2_t item, vec2_t min) => new(Math.Min(item.X, min.X), Math.Min(item.Y, min.Y));
    [MethodImpl(INLINE)] public static vec3_t Min(this vec3_t item, number_t min) => new(Math.Min(item.X, min), Math.Min(item.Y, min), Math.Min(item.Z, min));
    [MethodImpl(INLINE)] public static vec3_t Min(this vec3_t item, vec3_t min) => new(Math.Min(item.X, min.X), Math.Min(item.Y, min.Y), Math.Min(item.Z, min.Z));

    #endregion

    #region Getting
    [MethodImpl(INLINE)] public static vec2_t GetXY(this vec3_t item) => new(item.X, item.Y);
    [MethodImpl(INLINE)] public static vec2_t GetXZ(this vec3_t item) => new(item.X, item.Z);
    [MethodImpl(INLINE)] public static vec2_t GetYZ(this vec3_t item) => new(item.X, item.Z);
    #endregion
}
