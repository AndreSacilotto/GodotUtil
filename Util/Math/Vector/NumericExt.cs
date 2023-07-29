using System.Numerics;

namespace Util;

public static class NumericExt
{
    public static Vector2 Rotated(this Vector2 vec, float angle)
    {
        (float sin, float cos) = MathF.SinCos(angle);
        return new Vector2
        (
            vec.X * cos - vec.Y * sin,
            vec.X * sin + vec.Y * cos
        );
    }

    public static Vector2 Normalized(this Vector2 vec)
    {
        return Vector2.Normalize(vec);
        //real_t lengthsq = vec.LengthSquared();
        //if (lengthsq == 0)
        //{
        //    vec.X = vec.Y = 0f;
        //}
        //else
        //{
        //    real_t length = MathF.Sqrt(lengthsq);
        //    vec.X /= length;
        //    vec.Y /= length;
        //}
    }

    public static float Cross(this Vector2 vec, Vector2 with) => 
        (vec.X * with.Y) - (vec.Y * with.X);

    public static float DistanceTo(this Vector2 from, Vector2 to) => 
        Vector2.Distance(from, to);

    public static float AngleTo(this Vector2 from, Vector2 to) => 
        MathF.Atan2(Cross(from, to), Vector2.Dot(from, to));

    public static float Angle(this Vector2 vec) => MathF.Atan2(vec.Y, vec.X);
}
