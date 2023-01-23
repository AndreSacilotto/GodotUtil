using System;
using Util;

#if REAL_T_IS_DOUBLE
using real_t = System.Double;
#else
using real_t = System.Single;
#endif

namespace Godot
{
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
				basis.Row0[0] * vector.x + basis.Row1[0] * vector.y + basis.Row2[0] * vector.z,
				basis.Row0[1] * vector.x + basis.Row1[1] * vector.y + basis.Row2[1] * vector.z,
				basis.Row0[2] * vector.x + basis.Row1[2] * vector.y + basis.Row2[2] * vector.z
			);
		}

		/// <summary>
		/// Returns a perpendicular vector rotated 90 degrees counter-clockwise
		/// compared to the original, with the same length.
		/// </summary>
		/// <returns>The perpendicular vector.</returns>
		public static Vector2 Orthogonal(this Vector2 vec) => new(vec.y, -vec.x);

		#endregion

		#region INT UNIQUE

		#region Direction

		public static bool IsDiagonal(this Vector2i dir) => dir.x != 0 && dir.y != 0;
		public static bool IsStraight(this Vector2i dir) => dir.x == 0 || dir.y == 0;

		public static Vector2i PositionToDiagonal(Vector2 position, Vector2 center = default)
		{
			var rad = MathF.Atan2(position.y - center.y, position.x - center.x);
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
		public static Vector2i PositionToStraight(Vector2 position, Vector2 center = default)
		{
			var rad = MathF.PI - MathF.Atan2(center.y - position.y, center.x - position.x);
			if (rad <= UtilMath.TAU_45)
				return Vector2i.Right;
			else if (rad <= UtilMath.TAU_135)
				return Vector2i.Up;
			else if (rad <= UtilMath.TAU_45 * 5f)
				return Vector2i.Left;
			else if (rad <= UtilMath.TAU_45 * 7f)
				return Vector2i.Down;
			return Vector2i.Right;
		}

		#endregion

		#region Rounding

		public static Vector2i RoundToInt(float x, float y) => new(UtilMath.RoundToInt(x), UtilMath.RoundToInt(y));
		public static Vector2i FloorToInt(float x, float y) => new(UtilMath.FloorToInt(x), UtilMath.FloorToInt(y));
		public static Vector2i CeilToInt(float x, float y) => new(UtilMath.CeilToInt(x), UtilMath.CeilToInt(y));

		public static Vector3i RoundToInt(float x, float y, float z) => new(UtilMath.RoundToInt(x), UtilMath.RoundToInt(y), UtilMath.RoundToInt(z));
		public static Vector3i FloorToInt(float x, float y, float z) => new(UtilMath.FloorToInt(x), UtilMath.FloorToInt(y), UtilMath.FloorToInt(z));
		public static Vector3i CeilToInt(float x, float y, float z) => new(UtilMath.CeilToInt(x), UtilMath.CeilToInt(y), UtilMath.FloorToInt(z));

		#endregion

		#region Invert
		/// <summary>Returns new Vector(y, x)</summary>
		public static Vector2 SwapXY(this Vector2 v) => new(v.y, v.x);
		/// <summary>Returns new Vector(y, x)</summary>
		public static Vector2i SwapXY(this Vector2i v) => new(v.y, v.x);
		#endregion

		#endregion INT UNIQUE

		#region FLOAT UNIQUE

		#region Rotation
		// CC = Counter-Clockwise

		public static Vector2 RotatedCC(this Vector2 vec, real_t angle)
		{
			var sin = -MathF.Sin(angle);
			var cos = MathF.Cos(angle);
			return new(vec.x * cos - vec.y * sin, vec.x * sin + vec.y * cos);
		}

		public static Vector3 RotatedCC(this Vector3 vec, Vector3 axis, real_t angle) => new Basis(axis, -angle).Mult(vec);

		#endregion

		#endregion FLOAT UNIQUE

	}
}
