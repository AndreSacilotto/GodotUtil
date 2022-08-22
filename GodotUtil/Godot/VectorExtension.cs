using System.Runtime.CompilerServices;

namespace Godot
{
    public static class VectorExtension
    {
        private const MethodImplOptions INLINE = MethodImplOptions.AggressiveInlining;

        public static void SetX(ref Vector2 item, float x) => item = new Vector2(x, item.y);
        public static void SetY(ref Vector2 item, float y) => item = new Vector2(item.x, y);

        [MethodImpl(INLINE)] public static Vector2 SetX(this Vector2 item, float x) => new Vector2(x, item.y);
        [MethodImpl(INLINE)] public static Vector2 SetY(this Vector2 item, float y) => new Vector2(item.x, y);

        [MethodImpl(INLINE)] public static Vector2 NegateX(this Vector2 item) => new Vector2(-item.x, item.y);
        [MethodImpl(INLINE)] public static Vector2 NegateY(this Vector2 item) => new Vector2(item.x, -item.y);

        [MethodImpl(INLINE)] public static Vector2 Add(this Vector2 item, float value) => new Vector2(item.x + value, item.y + value);
        [MethodImpl(INLINE)] public static Vector2 Add(this Vector2 item, int value) => new Vector2(item.x + value, item.y + value);

        public static Vector2i Round(float x, float y) => new Vector2i(Mathf.RoundToInt(x), Mathf.RoundToInt(y));
        public static Vector2i Floor(float x, float y) => new Vector2i(Mathf.FloorToInt(x), Mathf.FloorToInt(y));
        public static Vector2i Ceil(float x, float y) => new Vector2i(Mathf.CeilToInt(x), Mathf.CeilToInt(y));

    }
}
