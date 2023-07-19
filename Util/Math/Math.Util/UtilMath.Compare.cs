using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Util;

public static partial class UtilMath
{
    #region Precision Float

    [MethodImpl(INLINE)] public static bool MoreThanZero(this float value) => value > EPSILON_FLOAT;
    [MethodImpl(INLINE)] public static bool LessThanZero(this float value) => value < -EPSILON_FLOAT;

    [MethodImpl(INLINE)] public static bool ApproximatelySign(float a, float b) => Math.Abs(b - a) < EPSILON_FLOAT;//float.Epsilon;

    public static bool AlmostEqual2sComplement(float a, float b, int maxDeltaBits = 6)
    {
        int aInt = FloatAsInt.Convert(a);
        if (aInt < 0)
            aInt = Int32.MinValue - aInt;

        int bInt = FloatAsInt.Convert(b);
        if (bInt < 0)
            bInt = Int32.MinValue - bInt;

        return Math.Abs(aInt - bInt) <= (1 << maxDeltaBits);
    }

    [StructLayout(LayoutKind.Explicit)]
    public readonly struct FloatAsInt
    {
        public static int Convert(float value) => new FloatAsInt(value).IntValue;
        public FloatAsInt(float floatValue) => FloatValue = floatValue;
        [FieldOffset(0)] public readonly int IntValue;
        [FieldOffset(0)] public readonly float FloatValue;
    }

    #endregion

    #region Bool to Value
    [MethodImpl(INLINE)] public static T BoolValue01<T>(bool value) where T : INumber<T> => value ? T.Zero : T.One;
    [MethodImpl(INLINE)] public static T BoolValue10<T>(bool value) where T : INumber<T> => value ? T.One : T.Zero;
    [MethodImpl(INLINE)] public static T BoolValue11<T>(bool value) where T : INumber<T>, ISignedNumber<T> => value ? T.One : T.NegativeOne;
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

    public static T Smaller<T>(params T[] values) where T : IComparable<T>
    {
        int len = values.Length;
        var smaller = values[0];
        for (int i = 1; i < len; i++)
            if (values[i].CompareTo(smaller) < 0)
                smaller = values[i];
        return smaller;
    }
    public static T Biggest<T>(params T[] values) where T : IComparable<T>
    {
        int len = values.Length;
        var biggest = values[0];
        for (int i = 1; i < len; i++)
            if (values[i].CompareTo(biggest) > 0)
                biggest = values[i];
        return biggest;
    }

    #endregion

}