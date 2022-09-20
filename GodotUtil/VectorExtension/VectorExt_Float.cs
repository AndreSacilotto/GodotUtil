using System.Runtime.CompilerServices;
using Util;

namespace Godot
{
    public static partial class VectorExt
    {

        #region FLOAT UNIQUE

        #region Rotation
        // CC = Counter-Clockwise
        
        public static Vector2 RotatedCC(this Vector2 vec, float angle) 
        {
            var sin = -Mathf.Sin(angle);
            var cos = Mathf.Cos(angle);
            return new(vec.x * cos - vec.y * sin, vec.x * sin + vec.y * cos);
        }

        public static Vector3 RotatedCC(this Vector3 vec, Vector3 axis, float angle) => new Basis(axis, -angle).Mult(vec);

        #endregion

        #endregion FLOAT UNIQUE

        #region New

        [MethodImpl(UtilShared.INLINE)] public static Vector2 CreateVec2(float value) => new(value, value);

        [MethodImpl(UtilShared.INLINE)] public static Vector3 CreateVec3(float x, float y, float z = 0f) => new(x, y, z);
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

    }
}
