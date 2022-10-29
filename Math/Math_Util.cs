using System;
using System.Runtime.CompilerServices;

using static Util.UtilShared;

namespace Util
{

	public static partial class UtilMath
	{

		#region Pow

		[MethodImpl(INLINE)] public static int PowSquare(int value) => value * value;
		[MethodImpl(INLINE)] public static float PowSquare(float value) => value * value;
		[MethodImpl(INLINE)] public static double PowSquare(double value) => value * value;

		[MethodImpl(INLINE)] public static int PowCubic(int value) => value * value * value;
		[MethodImpl(INLINE)] public static float PowCubic(float value) => value * value * value;
		[MethodImpl(INLINE)] public static double PowCubic(double value) => value * value * value;

		[MethodImpl(INLINE)] public static int PowQuartic(int value) => value * value * value * value;
		[MethodImpl(INLINE)] public static float PowQuartic(float value) => value * value * value * value;
		[MethodImpl(INLINE)] public static double PowQuartic(double value) => value * value * value * value;

		[MethodImpl(INLINE)] public static int PowQuintic(int value) => value * value * value * value * value;
		[MethodImpl(INLINE)] public static float PowQuintic(float value) => value * value * value * value * value;
		[MethodImpl(INLINE)] public static double PowQuintic(double value) => value * value * value * value * value;

		#endregion

		#region Precision

		[MethodImpl(INLINE)] public static float CorrectPrecision(float value) => MathF.Round(value, 6);

		#endregion

		#region Rounding

		[MethodImpl(INLINE)] public static float RoundToNearestHalf(float value) => MathF.Round(value * 2f) / 2f;

		[MethodImpl(INLINE)] public static int EvenCeil(int value) => value % 2 != 0 ? ++value : value;
		[MethodImpl(INLINE)] public static int EvenFloor(int value) => value % 2 != 0 ? --value : value;
		[MethodImpl(INLINE)] public static int OddCeil(int value) => value % 2 == 0 ? ++value : value;
		[MethodImpl(INLINE)] public static int OddFloor(int value) => value % 2 == 0 ? --value : value;

		[MethodImpl(INLINE)] public static int FloorToInt(float value) => (int)MathF.Floor(value);
		[MethodImpl(INLINE)] public static int FloorToInt(double value) => (int)Math.Floor(value);
		[MethodImpl(INLINE)] public static int RoundToInt(float value) => (int)MathF.Round(value);
		[MethodImpl(INLINE)] public static int RoundToInt(double value) => (int)Math.Round(value);
		[MethodImpl(INLINE)] public static int CeilToInt(float value) => (int)MathF.Ceiling(value);
		[MethodImpl(INLINE)] public static int CeilToInt(double value) => (int)Math.Ceiling(value);

		#endregion

		#region Clamping

		[MethodImpl(INLINE)]
		public static int Clamp(int value, int min, int max)
		{
			if (value > max)
				return max;
			if (value < min)
				return min;
			return value;
		}

		[MethodImpl(INLINE)]
		public static float Clamp(float value, float min, float max)
		{
			if (value > max)
				return max;
			if (value < min)
				return min;
			return value;
		}
		[MethodImpl(INLINE)]
		public static float Clamp01(float value)
		{
			if (value > 1f)
				return 1f;
			if (value < 0f)
				return 0f;
			return value;
		}

		public static float Confine(float value, float min, float max, out float excess)
		{
			if (value < min)
			{
				excess = -Math.Abs(value - min);
				return min;
			}
			if (value > max)
			{
				excess = Math.Abs(value - max);
				return max;
			}
			excess = 0;
			return value;
		}

		#endregion

		#region Digit

		public static int GetDigit(int value, int power, int radix) => (int)(value / MathF.Pow(radix, power)) % radix;

		[MethodImpl(INLINE)] public static float Fract(float x) => x - MathF.Floor(x);

		#endregion

	}

}