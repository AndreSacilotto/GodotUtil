using System.Runtime.CompilerServices;

namespace Util;

public static partial class UtilMath
{
	#region Precision Float

	[MethodImpl(INLINE)] public static bool MoreThanZero(this float value) => value > EPSILON_FLOAT;
	[MethodImpl(INLINE)] public static bool LessThanZero(this float value) => value < -EPSILON_FLOAT;

	[MethodImpl(INLINE)] public static bool Approximately(float a, float b) => Math.Abs(b - a) < EPSILON_FLOAT;//float.Epsilon;

	#endregion

	#region Bool to Value
	[MethodImpl(INLINE)] public static int BoolValue01(bool value) => value ? 0 : 1;

	[MethodImpl(INLINE)] public static int BoolValue10(bool value) => value ? 1 : 0;

	[MethodImpl(INLINE)] public static int BoolValue11(bool value) => value ? 1 : -1;
	#endregion

	#region Min Max

	[MethodImpl(INLINE)]
	public static void BiggerAndSmaller<T>(T v1, T v2, out T smaller, out T bigger) where T : IComparable<T>
	{
		if (v1.CompareTo(v2) > 0)
		{
			bigger = v1;
			smaller = v2;
		}
		else
		{
			bigger = v2;
			smaller = v1;
		}
	}

	[MethodImpl(INLINE)]
	public static T BiggerOne<T>(T v1, T v2) where T : IComparable<T>
	{
		if (v1.CompareTo(v2) > 0)
			return v1;
		return v2;
	}

	[MethodImpl(INLINE)]
	public static T SmallerOne<T>(T v1, T v2) where T : IComparable<T>
	{
		if (v1.CompareTo(v2) > 0)
			return v1;
		return v2;
	}

	public static T Min<T>(params T[] values) where T : IComparable<T>
	{
		int len = values.Length;
		var smaller = values[0];
		for (int i = 1; i < len; i++)
			if (values[i].CompareTo(smaller) < 0)
				smaller = values[i];
		return smaller;
	}
	public static T Max<T>(params T[] values) where T : IComparable<T>
	{
		int len = values.Length;
		var biggest = values[0];
		for (int i = 1; i < len; i++)
			if (values[i].CompareTo(biggest) > 0)
				biggest = values[i];
		return biggest;
	}

	#endregion

	#region Even Odd
	[MethodImpl(INLINE)] public static bool IsEven(int value) => value % 2 == 0;
	[MethodImpl(INLINE)] public static bool IsOdd(int value) => value % 2 != 0;

	// Do not use any of the below, trust in the compiler (He is better than you)
	//public static bool IsEvenBitwise(int value) => (value & 1) == 0;
	//public static bool IsEvenBitShifting(int value) => ((value >> 1) << 1) == value;

	#endregion

}