using System;
using System.Runtime.CompilerServices;

using Vector2 = Godot.Vector2;
using Vector3 = Godot.Vector3;
using Quarternion = Godot.Quat;

namespace Util.Vector
{
    public static class VectorMath
    {
        public static Vector2 RadianToVector2(float radian) => new((float)Math.Cos(radian), (float)Math.Sin(radian));

        public static Vector2 DegreeToVector(float degree) => RadianToVector2(UtilMath.TAU_01 * degree);

        [MethodImpl(UtilShared.INLINE)]
        public static Vector3 DegreeToVector(float degree, Vector3 axis) => new Quarternion(axis, degree).GetEuler();

        public static float TauAtan2(Vector2 vector) => TauAtan2(vector.y, vector.x);
        public static float TauAtan2(float y, float x) => (float)Math.Atan2(y, x) + UtilMath.TAU_180;

        [MethodImpl(UtilShared.INLINE)]
        public static Vector2 RotatedNoTrig(in Vector2 vec, in float cos, in float sin) => 
            new(vec.x * cos - vec.y * sin, vec.x * sin + vec.y * cos);

        [MethodImpl(UtilShared.INLINE)]
        public static Vector2 RotatedNoTrig(in Vector2 vec, in Vector2 pivot, in float cos, in float sin)
        {
            var x = vec.x - pivot.x;
            var y = vec.y - pivot.y;
            return new Vector2(pivot.x + x * cos - y * sin, pivot.y + x * sin + y * cos);
        }

    }

}
