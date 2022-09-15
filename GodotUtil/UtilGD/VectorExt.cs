using System.Runtime.CompilerServices;
using Util;

namespace Godot
{
    public static class VectorExt
    {

        #region Unique

        #region From Godot 4.0

        /// <summary>
        /// https://github.com/godotengine/godot/blob/2d9583fa3bf2426dabbdd9e9d9b8fd9725b9436c/modules/mono/glue/GodotSharp/GodotSharp/Core/Basis.cs#L861
        /// </summary>
        public static Vector3 Mult(this Basis basis, Vector3 vector)
        {
            return new Vector3
            (
                basis.Row0[0] * vector.x + basis.Row1[0] * vector.y + basis.Row2[0] * vector.z,
                basis.Row0[1] * vector.x + basis.Row1[1] * vector.y + basis.Row2[1] * vector.z,
                basis.Row0[2] * vector.x + basis.Row1[2] * vector.y + basis.Row2[2] * vector.z
            );
        }


        #endregion

        #region Rounding

        public static Vector2i Round(float x, float y) => new(Mathf.RoundToInt(x), Mathf.RoundToInt(y));
        public static Vector2i Floor(float x, float y) => new(Mathf.FloorToInt(x), Mathf.FloorToInt(y));
        public static Vector2i Ceil(float x, float y) => new(Mathf.CeilToInt(x), Mathf.CeilToInt(y));

        public static Vector3i Round(float x, float y, float z) => new(Mathf.RoundToInt(x), Mathf.RoundToInt(y), Mathf.RoundToInt(z));
        public static Vector3i Floor(float x, float y, float z) => new(Mathf.FloorToInt(x), Mathf.FloorToInt(y), Mathf.FloorToInt(z));
        public static Vector3i Ceil(float x, float y, float z) => new(Mathf.CeilToInt(x), Mathf.CeilToInt(y), Mathf.FloorToInt(z));

        #endregion

        #endregion

        #region FLOAT VERSION

        #region New

        [MethodImpl(UtilShared.INLINE)] public static Vector3 NewVec3(float x, float y, float z = 0f) => new(x, y, z);

        [MethodImpl(UtilShared.INLINE)] public static Vector2 CreateVec2(float value) => new(value, value);
        [MethodImpl(UtilShared.INLINE)] public static Vector3 CreateVec3(float value) => new(value, value, value);


        #endregion

        #region Setting

        [MethodImpl(UtilShared.INLINE)] public static Vector2 SetX(this Vector2 item, float x) => new(x, item.y);
        [MethodImpl(UtilShared.INLINE)] public static Vector2 SetY(this Vector2 item, float y) => new(item.x, y);

        [MethodImpl(UtilShared.INLINE)] public static Vector3 SetX(this Vector3 item, float x) => new(x, item.y, item.z);
        [MethodImpl(UtilShared.INLINE)] public static Vector3 SetY(this Vector3 item, float y) => new(item.x, y, item.z);
        [MethodImpl(UtilShared.INLINE)] public static Vector3 SetZ(this Vector3 item, float z) => new(item.x, item.y, z);

        [MethodImpl(UtilShared.INLINE)] public static Vector2 NegX(this Vector2 item) => new(-item.x, item.y);
        [MethodImpl(UtilShared.INLINE)] public static Vector2 NegY(this Vector2 item) => new(item.x, -item.y);

        [MethodImpl(UtilShared.INLINE)] public static Vector3 NegX(this Vector3 item) => new(-item.x, item.y, item.x);
        [MethodImpl(UtilShared.INLINE)] public static Vector3 NegY(this Vector3 item) => new(item.x, -item.y, item.y);
        [MethodImpl(UtilShared.INLINE)] public static Vector3 NegZ(this Vector3 item) => new(item.x, item.y, -item.z);

        #endregion

        #region Clamping

        [MethodImpl(UtilShared.INLINE)] public static Vector2 Clamp(this Vector2 item, float min, float max) => new(Mathf.Clamp(item.x, min, max), Mathf.Clamp(item.y, min, max));
        [MethodImpl(UtilShared.INLINE)] public static Vector2 Clamp(this Vector2 item, Vector2 min, Vector2 max) => new(Mathf.Clamp(item.x, min.x, max.x), Mathf.Clamp(item.y, min.y, max.y));

        [MethodImpl(UtilShared.INLINE)] public static Vector2 Max(this Vector2 item, float max) => new(Mathf.Max(item.x, max), Mathf.Max(item.y, max));
        [MethodImpl(UtilShared.INLINE)] public static Vector2 Max(this Vector2 item, Vector2 max) => new(Mathf.Max(item.x, max.x), Mathf.Max(item.y, max.y));
        
        [MethodImpl(UtilShared.INLINE)] public static Vector2 Min(this Vector2 item, float min) => new(Mathf.Min(item.x, min), Mathf.Min(item.y, min));
        [MethodImpl(UtilShared.INLINE)] public static Vector2 Min(this Vector2 item, Vector2 min) => new(Mathf.Min(item.x, min.x), Mathf.Min(item.y, min.y));


        #endregion

        #region Adding

        [MethodImpl(UtilShared.INLINE)] public static Vector2 AddX(this Vector2 item, float x) => new(item.x + x, item.y);
        [MethodImpl(UtilShared.INLINE)] public static Vector2 AddY(this Vector2 item, float y) => new(item.x, item.y + y);

        [MethodImpl(UtilShared.INLINE)] public static Vector3 AddX(this Vector3 item, float x) => new(item.x + x, item.y, item.z);
        [MethodImpl(UtilShared.INLINE)] public static Vector3 AddY(this Vector3 item, float y) => new(item.x, item.y + y, item.z);
        [MethodImpl(UtilShared.INLINE)] public static Vector3 AddZ(this Vector3 item, float z) => new(item.x, item.y, item.z + z);

        [MethodImpl(UtilShared.INLINE)] public static Vector2 Add(this Vector2 item, float value) => new(item.x + value, item.y + value);
        [MethodImpl(UtilShared.INLINE)] public static Vector3 Add(this Vector3 item, float value) => new(item.x + value, item.y + value, item.z + value);

        #endregion

        #region Converting

        [MethodImpl(UtilShared.INLINE)] public static Vector3 ToVector3_XY0(this Vector2 vector) => new(vector.x, vector.y, 0f);
        [MethodImpl(UtilShared.INLINE)] public static Vector3 ToVector3_X0Y(this Vector2 vector) => new(vector.x, 0f, vector.y);
        [MethodImpl(UtilShared.INLINE)] public static Vector2 ToVector2_XY(this Vector3 vector) => new(vector.x, vector.y);
        [MethodImpl(UtilShared.INLINE)] public static Vector2 ToVector2_XZ(this Vector3 vector) => new(vector.x, vector.z);

        #endregion

        #endregion FLOAT VERSION

        #region INT VERSION

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

        #endregion INT VERSION

    }
}
