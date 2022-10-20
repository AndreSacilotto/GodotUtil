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
        public static Vector2 RadianToVector2(float radian) => new(MathF.Cos(radian), MathF.Sin(radian));

        public static Vector2 DegreeToVector(float degree) => RadianToVector2(UtilMath.TAU_01 * degree);

        [MethodImpl(UtilShared.INLINE)]
        public static Vector3 DegreeToVector(float degree, Vector3 axis) => new Quarternion(axis, degree).GetEuler();

        public static float TauAtan2(Vector2 vector) => TauAtan2(vector.y, vector.x);
        public static float TauAtan2(float y, float x) => MathF.Atan2(y, x) + UtilMath.TAU_180;


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
            var s = MathF.Sin(rotation);
            var c = MathF.Cos(rotation);
            for (int i = 0; i < points.Length; i++)
                points[i] = RotatedNoTrigCW(points[i], c, s);
        }
        public static void RotateVectors(Vector2[] points, Vector2 pivot, float rotation)
        {
            var s = MathF.Sin(rotation);
            var c = MathF.Cos(rotation);
            for (int i = 0; i < points.Length; i++)
                points[i] = RotatedNoTrigCW(points[i], pivot, c, s);
        }

        public static void RotateVectors(Vector3[] points, float rotation, Vector3 axis)
        {
            var b = new Basis(axis, rotation);
            for (int i = 0; i < points.Length; i++)
                points[i] = Godot.VectorExt.Mult(b, points[i]);
        }

        #endregion

        #region Direction2D

        public static Vector2 PositionToFloatDirection(Vector2 position, Vector2 center = default)
        {
            var rad = MathF.Atan2(center.y - position.y, center.x - position.x) + UtilMath.TAU_180;
            return new Vector2(MathF.Cos(rad), MathF.Sin(rad));
        }
        public static Vector2i PositionToDirection(Vector2 position, Vector2 center = default)
        {
            var rad = MathF.Atan2(center.y - position.y, center.x - position.x) + UtilMath.TAU_180;
            return new(UtilMath.RoundToInt(MathF.Cos(rad)), UtilMath.RoundToInt(MathF.Sin(rad)));
        }

        #endregion

        public static Vector2[] GetTrajectoryArc(int count, float radians, Vector2 dir)
        {
            if (count < 0)
                return Array.Empty<Vector2>();

            var index = 0;
            var vectors = new Vector2[count];
            if (count % 2 != 0)
            {
                vectors[index++] = dir;
                count--;
            }

            count /= 2;
            for (int i = 0; i < count; i++)
            {
                var angle = (i + 1) * radians;
                var c = MathF.Cos(angle);
                var s = MathF.Sin(angle);

                vectors[index++] = RotatedNoTrigCW(dir, c, s);
                vectors[index++] = RotatedNoTrigCW(dir, c, -s);
            }

            return vectors;
        }


    }

}
