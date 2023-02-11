using System.Linq;
using System.Runtime.CompilerServices;

namespace Util;

public static partial class UtilMath
{

	#region GCD
	/// <summary>
	/// https://www.Geeksforgeeks.org/steins-algorithm-for-finding-gcd/
	/// </summary>
	public static int BinaryGCD(int a, int b)
	{
		if (a == 0)
			return b;
		if (b == 0)
			return a;

		int k;
		for (k = 0; ((a | b) & 1) == 0; ++k)
		{
			a >>= 1;
			b >>= 1;
		}

		while ((a & 1) == 0)
			a >>= 1;

		do
		{
			while ((b & 1) == 0)
				b >>= 1;
			if (a > b)
				(b, a) = (a, b);
			b -= a;
		} while (b != 0);

		return a << k;
	}

	/// <summary>Greatest common divisor</summary>
	public static int GCD(int a, int b)
	{
		while (a != 0 && b != 0)
		{
			if (a > b)
				a %= b;
			else
				b %= a;
		}
		return a | b;
	}
	#endregion

	#region Average

	[MethodImpl(INLINE)]
	public static int Average(IEnumerable<int> items)
	{
		int sum = 0, count = 0;
		foreach (var item in items)
		{
			sum += item;
			count++;
		}
		return sum / count;
	}
	[MethodImpl(INLINE)]
	public static float Average(IEnumerable<float> items)
	{
		float sum = 0;
		int count = 0;
		foreach (var item in items)
		{
			sum += item;
			count++;
		}
		return sum / count;
	}
	#endregion

	#region Median

	[MethodImpl(INLINE)] public static float FindMedian(float[] arr) => FindMedian(arr, 0, arr.Length);
	public static float FindMedian(float[] arr, int index, int length)
	{
		Array.Sort(arr, index, length);
		length += index;
		if (length % 2 != 0)
			return arr[length / 2 + index];
		return (arr[(length - 1) / 2 + index] + arr[length / 2] + index) / 2f;
	}

	public static float FindMedian(int[] arr) => FindMedian(arr, 0, arr.Length);
	public static float FindMedian(int[] arr, int index, int length)
	{
		Array.Sort(arr, index, length);
		length += index;
		if (length % 2 != 0)
			return arr[length / 2 + index];
		return (arr[(length - 1) / 2 + index] + arr[length / 2] + index) / 2f;
	}

	#endregion

	#region Average Related
	public static float StandardDeviation(IEnumerable<int> values)
	{
		var avg = Average(values);
		return MathF.Sqrt((float)values.Average(v => PowSquare(v - avg)));
	}
	public static float StandardDeviation(IEnumerable<float> values)
	{
		var avg = Average(values);
		return MathF.Sqrt(values.Average(v => PowSquare(v - avg)));
	}
	#endregion

}