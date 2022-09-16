using System.Runtime.CompilerServices;
using Util;

namespace Godot
{
    public static partial class VectorExt
    {
        #region INT UNIQUE

        #endregion INT UNIQUE

        #region New

        [MethodImpl(UtilShared.INLINE)] public static Vector3i NewVec3(int x, int y, int z = 0) => new(x, y, z);

        [MethodImpl(UtilShared.INLINE)] public static Vector2i CreateVec2(int value) => new(value, value);
        [MethodImpl(UtilShared.INLINE)] public static Vector3i CreateVec3(int value) => new(value, value, value);


        #endregion

        #region Setting

        [MethodImpl(UtilShared.INLINE)] public static Vector2i SetX(this Vector2i item, int x) => new(x, item.y);
        [MethodImpl(UtilShared.INLINE)] public static Vector2i SetY(this Vector2i item, int y) => new(item.x, y);

        [MethodImpl(UtilShared.INLINE)] public static Vector3i SetX(this Vector3i item, int x) => new(x, item.y, item.z);
        [MethodImpl(UtilShared.INLINE)] public static Vector3i SetY(this Vector3i item, int y) => new(item.x, y, item.z);
        [MethodImpl(UtilShared.INLINE)] public static Vector3i SetZ(this Vector3i item, int z) => new(item.x, item.y, z);

        [MethodImpl(UtilShared.INLINE)] public static Vector2i NegX(this Vector2i item) => new(-item.x, item.y);
        [MethodImpl(UtilShared.INLINE)] public static Vector2i NegY(this Vector2i item) => new(item.x, -item.y);

        [MethodImpl(UtilShared.INLINE)] public static Vector3i NegX(this Vector3i item) => new(-item.x, item.y, item.x);
        [MethodImpl(UtilShared.INLINE)] public static Vector3i NegY(this Vector3i item) => new(item.x, -item.y, item.y);
        [MethodImpl(UtilShared.INLINE)] public static Vector3i NegZ(this Vector3i item) => new(item.x, item.y, -item.z);

        #endregion

        #region Clamping

        [MethodImpl(UtilShared.INLINE)] public static Vector2i Clamp(this Vector2i item, int min, int max) => new(Mathf.Clamp(item.x, min, max), Mathf.Clamp(item.y, min, max));
        [MethodImpl(UtilShared.INLINE)] public static Vector2i Clamp(this Vector2i item, Vector2i min, Vector2i max) => new(Mathf.Clamp(item.x, min.x, max.x), Mathf.Clamp(item.y, min.y, max.y));

        [MethodImpl(UtilShared.INLINE)] public static Vector2i Max(this Vector2i item, int max) => new(Mathf.Max(item.x, max), Mathf.Max(item.y, max));
        [MethodImpl(UtilShared.INLINE)] public static Vector2i Max(this Vector2i item, Vector2i max) => new(Mathf.Max(item.x, max.x), Mathf.Max(item.y, max.y));

        [MethodImpl(UtilShared.INLINE)] public static Vector2i Min(this Vector2i item, int min) => new(Mathf.Min(item.x, min), Mathf.Min(item.y, min));
        [MethodImpl(UtilShared.INLINE)] public static Vector2i Min(this Vector2i item, Vector2i min) => new(Mathf.Min(item.x, min.x), Mathf.Min(item.y, min.y));


        #endregion

        #region Adding

        [MethodImpl(UtilShared.INLINE)] public static Vector2i AddX(this Vector2i item, int x) => new(item.x + x, item.y);
        [MethodImpl(UtilShared.INLINE)] public static Vector2i AddY(this Vector2i item, int y) => new(item.x, item.y + y);

        [MethodImpl(UtilShared.INLINE)] public static Vector3i AddX(this Vector3i item, int x) => new(item.x + x, item.y, item.z);
        [MethodImpl(UtilShared.INLINE)] public static Vector3i AddY(this Vector3i item, int y) => new(item.x, item.y + y, item.z);
        [MethodImpl(UtilShared.INLINE)] public static Vector3i AddZ(this Vector3i item, int z) => new(item.x, item.y, item.z + z);

        [MethodImpl(UtilShared.INLINE)] public static Vector2i Add(this Vector2i item, int value) => new(item.x + value, item.y + value);
        [MethodImpl(UtilShared.INLINE)] public static Vector3i Add(this Vector3i item, int value) => new(item.x + value, item.y + value, item.z + value);

        #endregion

        #region Converting

        [MethodImpl(UtilShared.INLINE)] public static Vector3i ToVector3i_XY0(this Vector2i vector) => new(vector.x, vector.y, 0);
        [MethodImpl(UtilShared.INLINE)] public static Vector3i ToVector3i_X0Y(this Vector2i vector) => new(vector.x, 0, vector.y);
        [MethodImpl(UtilShared.INLINE)] public static Vector2i ToVector2i_XY(this Vector3i vector) => new(vector.x, vector.y);
        [MethodImpl(UtilShared.INLINE)] public static Vector2i ToVector2i_XZ(this Vector3i vector) => new(vector.x, vector.z);

        #endregion

    }
}
