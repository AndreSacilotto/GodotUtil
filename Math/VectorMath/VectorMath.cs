using System.Runtime.CompilerServices;
using Godot;

namespace Util.Vector
{
    public static class VectorMath
    {
        public static Vector2 RadianToVector2(float radian) => new(Mathf.Cos(radian), Mathf.Sin(radian));

        public static Vector2 DegreeToVector2(float degree) => RadianToVector2(MathUtil.TAU_01 * degree);

        public static float TauAtan2(Vector2 vector) => TauAtan2(vector.y, vector.x);
        public static float TauAtan2(float y, float x) => Mathf.Atan2(y, x) + MathUtil.TAU_180;


        [MethodImpl(UtilShared.INLINE)]
        public static Vector2 RotatedNoTrig(in Vector2 vec, in float cos, in float sin)
        {
            return new Vector2(vec.x * cos - vec.y * sin, vec.x * sin + vec.y * cos);
        }

        [MethodImpl(UtilShared.INLINE)]
        public static Vector2 RotatedNoTrig(in Vector2 vec, in Vector2 pivot, in float cos, in float sin)
        {
            var x = vec.x - pivot.x;
            var y = vec.y - pivot.y;
            return new Vector2(pivot.x + x * cos - y * sin, pivot.y + x * sin + y * cos);
        }

    }

}
