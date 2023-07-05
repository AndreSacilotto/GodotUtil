using System.Runtime.CompilerServices;
using Util;
using Basis = Godot.Basis;
using Quarternion = Godot.Quaternion;
using trans3 = Godot.Transform3D;
using vec2 = Godot.Vector2;
using vec2i = Godot.Vector2I;
using vec3 = Godot.Vector3;

namespace GodotUtil;

public static class UtilVector
{
    [MethodImpl(INLINE)]
    public static vec2 RadianToVector2(float radian) => new(MathF.Cos(radian), MathF.Sin(radian));

    [MethodImpl(INLINE)]
    public static vec2 DegreeToVector(float degree) => RadianToVector2(UtilMath.TAU_01 * degree);

    [MethodImpl(INLINE)]
    public static vec3 AngleToVector(float angle, vec3 axis) => new Quarternion(axis, angle).GetEuler();

    public static float TauAtan2(vec2 vector) => TauAtan2(vector.Y, vector.X);
    public static float TauAtan2(float y, float x) => MathF.Atan2(y, x) + UtilMath.TAU_180;

    #region Rotate
    // CW = Clockwise | CC = CounterClockwise

    [MethodImpl(INLINE)]
    public static vec2 RotatedNoTrigCW(in vec2 vec, in float cos, in float sin) =>
        new(vec.X * cos - vec.Y * sin, vec.X * sin + vec.Y * cos);

    [MethodImpl(INLINE)]
    public static vec2 RotatedNoTrigCW(in vec2 vec, in vec2 pivot, in float cos, in float sin)
    {
        var x = vec.X - pivot.X;
        var y = vec.Y - pivot.Y;
        return new vec2(pivot.X + x * cos - y * sin, pivot.Y + x * sin + y * cos);
    }

    public static void RotateVectors(vec2[] points, float rotation)
    {
        var s = MathF.Sin(rotation);
        var c = MathF.Cos(rotation);
        for (int i = 0; i < points.Length; i++)
            points[i] = RotatedNoTrigCW(points[i], c, s);
    }
    public static void RotateVectors(vec2[] points, vec2 pivot, float rotation)
    {
        var s = MathF.Sin(rotation);
        var c = MathF.Cos(rotation);
        for (int i = 0; i < points.Length; i++)
            points[i] = RotatedNoTrigCW(points[i], pivot, c, s);
    }

    public static void RotateVectors(vec3[] points, float rotation, vec3 axis)
    {
        var b = new Basis(axis, rotation);
        for (int i = 0; i < points.Length; i++)
            points[i] = Godot.VectorExt.Mult(b, points[i]);
    }

    public static trans3 RotateAround(trans3 trans, vec3 point, vec3 axis, float angle)
    {
        var rotatedBasis = trans.Basis.Rotated(axis, angle);
        var rotatedOrigin = point + (trans.Origin - point).Rotated(axis, angle);
        return new(rotatedBasis, rotatedOrigin);
    }

    #endregion

    #region Direction2D

    public static vec2 PositionToFloatDirection(vec2 position, vec2 center = default)
    {
        var rad = MathF.Atan2(center.Y - position.Y, center.X - position.X) + UtilMath.TAU_180;
        return new vec2(MathF.Cos(rad), MathF.Sin(rad));
    }
    public static vec2i PositionToDirection(vec2 position, vec2 center = default)
    {
        var rad = MathF.Atan2(center.Y - position.Y, center.X - position.X) + UtilMath.TAU_180;
        return new(UtilMath.RoundToInt(MathF.Cos(rad)), UtilMath.RoundToInt(MathF.Sin(rad)));
    }

    #endregion

    public static vec2[] GetTrajectoryArc(int count, float radians, vec2 dir)
    {
        if (count < 0)
            return Array.Empty<vec2>();

        var index = 0;
        var vectors = new vec2[count];
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

    #region Slerp
    public static vec2 Slerp(vec2 start, vec2 end, float weight)
    {
        // Dot product - the cosine of the angle between 2 vectors.
        float dot = start.Dot(end);
        // Clamp it to be in the range of Acos(), because of float precision
        UtilMath.Clamp(dot, -1.0f, 1.0f);
        // Acos(dot) returns the angle between start and end,
        // And multiplying that by percent returns the angle between start and the final result.
        var theta = MathF.Acos(dot) * weight;
        var RelativeVec = (end - start * dot).Normalized();
        // Orthonormal basis the final result.
        return (start * MathF.Cos(theta)) + (RelativeVec * MathF.Sin(theta));
    }
    public static vec3 Slerp(vec3 start, vec3 end, float weight)
    {
        // Dot product - the cosine of the angle between 2 vectors.
        float dot = start.Dot(end);
        // Clamp it to be in the range of Acos(), because of float precision
        UtilMath.Clamp(dot, -1.0f, 1.0f);
        // Acos(dot) returns the angle between start and end,
        // And multiplying that by percent returns the angle between start and the final result.
        var theta = MathF.Acos(dot) * weight;
        var RelativeVec = (end - start * dot).Normalized();
        // Orthonormal basis the final result.
        return (start * MathF.Cos(theta)) + (RelativeVec * MathF.Sin(theta));
    }
    #endregion

}
