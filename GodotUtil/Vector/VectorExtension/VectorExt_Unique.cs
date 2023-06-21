using Util;

#if REAL_T_IS_DOUBLE
using real_t = System.Double;
#else
using real_t = System.Single;
#endif

namespace Godot;

public static partial class VectorExt
{

    #region From Godot 4.0

    /// <summary>
    /// https://github.com/godotengine/godot/blob/2d9583fa3bf2426dabbdd9e9d9b8fd9725b9436c/modules/mono/glue/GodotSharp/GodotSharp/Core/Basis.cs#L861
    /// </summary>
    public static Vector3 Mult(this Basis basis, Vector3 vector)
    {
        return new Vector3
        (
            basis.Row0[0] * vector.X + basis.Row1[0] * vector.Y + basis.Row2[0] * vector.Z,
            basis.Row0[1] * vector.X + basis.Row1[1] * vector.Y + basis.Row2[1] * vector.Z,
            basis.Row0[2] * vector.X + basis.Row1[2] * vector.Y + basis.Row2[2] * vector.Z
        );
    }

    /// <summary>
    /// Returns a perpendicular vector rotated 90 degrees counter-clockwise
    /// compared to the original, with the same length.
    /// </summary>
    /// <returns>The perpendicular vector.</returns>
    public static Vector2 Orthogonal(this Vector2 vec) => new(vec.Y, -vec.X);

    #endregion

    #region INT UNIQUE

    #region Direction

    public static bool IsDiagonal(this Vector2I dir) => dir.X != 0 && dir.Y != 0;
    public static bool IsStraight(this Vector2I dir) => dir.X == 0 || dir.Y == 0;

    public static Vector2I PositionToDiagonal(Vector2 position, Vector2 center = default)
    {
        var rad = MathF.Atan2(position.Y - center.Y, position.X - center.X);
        if (rad < 0)
        {
            if (rad >= -UtilMath.TAU_90)
                return TopRight;
            return TopLeft;
        }
        else
        {
            if (rad <= UtilMath.TAU_90)
                return BottomRight;
            return BottomLeft;
        }
    }
    public static Vector2I PositionToStraight(Vector2 position, Vector2 center = default)
    {
        var rad = MathF.PI - MathF.Atan2(center.Y - position.Y, center.X - position.X);
        if (rad <= UtilMath.TAU_45)
            return Vector2I.Right;
        else if (rad <= UtilMath.TAU_135)
            return Vector2I.Up;
        else if (rad <= UtilMath.TAU_45 * 5f)
            return Vector2I.Left;
        else if (rad <= UtilMath.TAU_45 * 7f)
            return Vector2I.Down;
        return Vector2I.Right;
    }

    #endregion

    #region Rounding

    public static Vector2I RoundToInt(float x, float y) => new(UtilMath.RoundToInt(x), UtilMath.RoundToInt(y));
    public static Vector2I FloorToInt(float x, float y) => new(UtilMath.FloorToInt(x), UtilMath.FloorToInt(y));
    public static Vector2I CeilToInt(float x, float y) => new(UtilMath.CeilToInt(x), UtilMath.CeilToInt(y));

    public static Vector3I RoundToInt(float x, float y, float z) => new(UtilMath.RoundToInt(x), UtilMath.RoundToInt(y), UtilMath.RoundToInt(z));
    public static Vector3I FloorToInt(float x, float y, float z) => new(UtilMath.FloorToInt(x), UtilMath.FloorToInt(y), UtilMath.FloorToInt(z));
    public static Vector3I CeilToInt(float x, float y, float z) => new(UtilMath.CeilToInt(x), UtilMath.CeilToInt(y), UtilMath.FloorToInt(z));

    #endregion

    #region Invert
    /// <summary>Returns new Vector(y, x)</summary>
    public static Vector2 SwapXY(this Vector2 v) => new(v.Y, v.X);
    /// <summary>Returns new Vector(y, x)</summary>
    public static Vector2I SwapXY(this Vector2I v) => new(v.Y, v.X);
    #endregion

    #endregion INT UNIQUE

    #region FLOAT UNIQUE

    #region Rotation
    // CC = Counter-Clockwise

    public static Vector2 RotatedCC(this Vector2 vec, real_t angle)
    {
        var sin = -MathF.Sin(angle);
        var cos = MathF.Cos(angle);
        return new(vec.X * cos - vec.Y * sin, vec.X * sin + vec.Y * cos);
    }

    public static Vector3 RotatedCC(this Vector3 vec, Vector3 axis, real_t angle) => new Basis(axis, -angle).Mult(vec);

    #endregion

    #endregion FLOAT UNIQUE

}
