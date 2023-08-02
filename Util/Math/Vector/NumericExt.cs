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

    public static Vector2 Normalized(this Vector2 vec) => Vector2.Normalize(vec);
    public static Vector2 GD_Normalized(this Vector2 vec) 
    {
        float lengthsq = vec.LengthSquared();
        if (lengthsq == 0)
            return Vector2.Zero;

        float length = MathF.Sqrt(lengthsq);
        vec.X /= length;
        vec.Y /= length;
        return vec;
    }

    public static float Cross(this Vector2 vec, Vector2 with) => (vec.X * with.Y) - (vec.Y * with.X);

    public static float DistanceTo(this Vector2 from, Vector2 to) => Vector2.Distance(from, to);

    public static float AngleTo(this Vector2 from, Vector2 to) => MathF.Atan2(Cross(from, to), Vector2.Dot(from, to));

    public static float Angle(this Vector2 vec) => MathF.Atan2(vec.Y, vec.X);
}
