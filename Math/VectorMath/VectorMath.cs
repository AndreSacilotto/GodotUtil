using System;
using System.Runtime.CompilerServices;

using Vector2 = Godot.Vector2;
using Vector3 = Godot.Vector3;
using Vector2i = Godot.Vector2i;
using Vector3i = Godot.Vector3i;
using Quarternion = Godot.Quat;
using Basis = Godot.Basis;

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


        #region Rotate
        // CW = Clockwise | CC = CounterClockwise

        [MethodImpl(UtilShared.INLINE)]
        public static Vector2 RotatedNoTrigCW(in Vector2 vec, in float cos, in float sin) => 
            new(vec.x * cos - vec.y * sin, vec.x * sin + vec.y * cos);

        [MethodImpl(UtilShared.INLINE)]
        public static Vector2 RotatedNoTrigCW(in Vector2 vec, in Vector2 pivot, in float cos, in float sin)
        {
            var x = vec.x - pivot.x;
            var y = vec.y - pivot.y;
            return new Vector2(pivot.x + x * cos - y * sin, pivot.y + x * sin + y * cos);
        }

        public static void RotateVectors(Vector2[] points, float rotation)
        {
            var s = (float)Math.Sin(rotation);
            var c = (float)Math.Cos(rotation);
            for (int i = 0; i < points.Length; i++)
                points[i] = RotatedNoTrigCW(points[i], c, s);
        }
        public static void RotateVectors(Vector2[] points, Vector2 pivot, float rotation)
        {
            var s = (float)Math.Sin(rotation);
            var c = (float)Math.Cos(rotation);
            for (int i = 0; i < points.Length; i++)
                points[i] = RotatedNoTrigCW(points[i], pivot, c, s);
        }

        public static void RotateVectors(Vector3[] points, float rotation, Vector3 axis)
        {
            var b = new Basis(axis, rotation);
            for (int i = 0; i < points.Length; i++)
                points[i] = Godot.Godot.VectorExt.Mult(b, points[i]);
        }

        #endregion

        #region Direction2D

        public static Vector2 PositionToFloatDirection(Vector2 position, Vector2 center = default)
        {
            var rad = Math.Atan2(center.y - position.y, center.x - position.x) + Math.PI;
            return new Vector2((float)Math.Cos(rad), (float)Math.Sin(rad));
        }
        public static Vector2i PositionToDirection(Vector2 position, Vector2 center = default)
        {
            var rad = Math.Atan2(center.y - position.y, center.x - position.x) + Math.PI;
            return new(UtilMath.RoundToInt(Math.Cos(rad)), UtilMath.RoundToInt(Math.Sin(rad)));
        }

        #endregion

    }

}
