using System.Numerics;
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
    public static T Average<T>(IEnumerable<T> items) where T : INumber<T>
    {
        T sum = T.Zero;
        int count = 0;
        foreach (var item in items)
        {
            sum += item;
            count++;
        }
        return sum / T.CreateChecked(count);
    }
    [MethodImpl(INLINE)]
    public static T Average<T>(ICollection<T> items) where T : INumber<T>
    {
        T sum = T.Zero;
        foreach (var item in items)
            sum += item;
        return sum / T.CreateChecked(items.Count);
    }
    #endregion

    #region Median

    public static T GetMedian<T>(T[] sourceArray, bool cloneArray = true) where T : INumber<T>
    {
        var sortedArray = cloneArray ? (T[])sourceArray.Clone() : sourceArray;
        Array.Sort(sortedArray);

        int size = sortedArray.Length;
        (int mid, int remainer) = Math.DivRem(size, 2);
        if (remainer != 0) // It would be > 0 if was even 
            return sortedArray[mid];
        return (sortedArray[mid] + sortedArray[mid - 1]) / T.CreateChecked(2);
    }

    #endregion

    #region Average Related
    
    public static T StandardDeviation<T>(IEnumerable<T> values) where T : IFloatingPointIeee754<T>
    {
        var avg = Average(values);
        var deviations = T.Zero;
        int count = 0;
        foreach (var item in values)
        {
            deviations += PowSquare(item - avg);
            count++;
        }
        var variance = deviations / T.CreateChecked(count);
        return T.Sqrt(variance);
    }

    public static T StandardDeviation<T>(ICollection<T> values) where T : IFloatingPointIeee754<T>
    {
        var avg = Average(values);
        var deviations = T.Zero;
        foreach (var item in values)
            deviations += PowSquare(item - avg);
        var variance = deviations / T.CreateChecked(values.Count);
        return T.Sqrt(variance);
    }

    #endregion

}