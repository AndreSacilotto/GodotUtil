using System.Runtime.CompilerServices;

namespace Godot
{
    public static class VectorExtension
    {
        private const MethodImplOptions INLINE = MethodImplOptions.AggressiveInlining;

        #region New
        public static Vector2 Create(float value) => new(value, value);
        public static Vector2i Create(int value) => new(value, value);

        #endregion

        #region Setting

        public static void SetX(ref Vector2 item, float x) => item = new(x, item.y);
        public static void SetY(ref Vector2 item, float y) => item = new(item.x, y);

        [MethodImpl(INLINE)] public static Vector2 SetX(this Vector2 item, float x) => new(x, item.y);
        [MethodImpl(INLINE)] public static Vector2 SetY(this Vector2 item, float y) => new(item.x, y);

        [MethodImpl(INLINE)] public static Vector2 NegateX(this Vector2 item) => new(-item.x, item.y);
        [MethodImpl(INLINE)] public static Vector2 NegateY(this Vector2 item) => new(item.x, -item.y);

        #endregion

        #region Clamping

        [MethodImpl(INLINE)] public static Vector2 Clamp(this Vector2 item, float min, float max) => new(Mathf.Clamp(item.x, min, max), Mathf.Clamp(item.y, min, max));
        [MethodImpl(INLINE)] public static Vector2 Clamp(this Vector2 item, Vector2 min, Vector2 max) => new(Mathf.Clamp(item.x, min.x, max.x), Mathf.Clamp(item.y, min.y, max.y));

        [MethodImpl(INLINE)] public static Vector2 Max(this Vector2 item, float max) => new(Mathf.Max(item.x, max), Mathf.Max(item.y, max));
        [MethodImpl(INLINE)] public static Vector2 Max(this Vector2 item, Vector2 max) => new(Mathf.Max(item.x, max.x), Mathf.Max(item.y, max.y));
        
        [MethodImpl(INLINE)] public static Vector2 Min(this Vector2 item, float min) => new(Mathf.Min(item.x, min), Mathf.Min(item.y, min));
        [MethodImpl(INLINE)] public static Vector2 Min(this Vector2 item, Vector2 min) => new(Mathf.Min(item.x, min.x), Mathf.Min(item.y, min.y));


        #endregion

        #region Adding

        [MethodImpl(INLINE)] public static Vector2 AddX(this Vector2 item, float x) => new(item.x + x, item.y);
        [MethodImpl(INLINE)] public static Vector2 AddY(this Vector2 item, float y) => new(item.x, item.y + y);

        [MethodImpl(INLINE)] public static Vector2i AddX(this Vector2i item, int x) => new(item.x + x, item.y);
        [MethodImpl(INLINE)] public static Vector2i AddY(this Vector2i item, int y) => new(item.x, item.y + y);

        [MethodImpl(INLINE)] public static Vector2 Add(this Vector2 item, float value) => new(item.x + value, item.y + value);
        [MethodImpl(INLINE)] public static Vector2i Add(this Vector2i item, int value) => new(item.x + value, item.y + value);

        #endregion

        #region Rounding

        public static Vector2i Round(float x, float y) => new(Mathf.RoundToInt(x), Mathf.RoundToInt(y));
        public static Vector2i Floor(float x, float y) => new(Mathf.FloorToInt(x), Mathf.FloorToInt(y));
        public static Vector2i Ceil(float x, float y) => new(Mathf.CeilToInt(x), Mathf.CeilToInt(y));
        
        #endregion

    }
}
