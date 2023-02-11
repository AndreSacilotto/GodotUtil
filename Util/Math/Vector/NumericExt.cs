using System.Numerics;

#if REAL_T_IS_DOUBLE
using real_t = System.Double;
#else
using real_t = System.Single;
#endif

namespace Util;

public static class NumericExt
{
	public static Vector2 Rotated(this Vector2 vec, real_t angle)
	{
		(real_t sin, real_t cos) = MathF.SinCos(angle);
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

	public static real_t Cross(this Vector2 vec, Vector2 with)
	{
		return (vec.X * with.Y) - (vec.Y * with.X);
	}

	public static real_t DistanceTo(this Vector2 from, Vector2 to)
	{
		return Vector2.Distance(from, to);
		//return MathF.Sqrt((from.X - to.X) * (from.X - to.X) + (from.Y - to.Y) * (from.Y - to.Y));
	}

	public static real_t AngleTo(this Vector2 from, Vector2 to)
	{
		return MathF.Atan2(Cross(from, to), Vector2.Dot(from, to));
	}

	public static real_t Angle(this Vector2 vec)
	{
		return MathF.Atan2(vec.Y, vec.X);
	}




}
