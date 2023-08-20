using System.Numerics;
using System.Runtime.CompilerServices;

namespace Util;

public static partial class UtilMath
{

    #region Pow

    [MethodImpl(INLINE)] public static T PowSquare<T>(T value) where T : INumber<T> => value * value;
    [MethodImpl(INLINE)] public static T PowCubic<T>(T value) where T : INumber<T> => value * value * value;
    [MethodImpl(INLINE)] public static T PowQuartic<T>(T value) where T : INumber<T> => value * value * value * value;
    [MethodImpl(INLINE)] public static T PowQuintic<T>(T value) where T : INumber<T> => value * value * value * value * value;

    #endregion

    #region Precision

    [MethodImpl(INLINE)] public static float CorrectPrecision(float value) => MathF.Round(value, 6);
    [MethodImpl(INLINE)] public static double CorrectPrecision(double value) => Math.Round(value, 12);

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

    [MethodImpl(INLINE)] public static int TruncateToInt(float value) => (int)MathF.Truncate(value);
    [MethodImpl(INLINE)] public static int TruncateToInt(double value) => (int)Math.Truncate(value);

    #endregion

    #region Clamping

    [MethodImpl(INLINE)] public static T Clamp01<T>(T value) where T : INumber<T> => T.Clamp(value, T.One, T.Zero);

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

    [MethodImpl(INLINE)] public static float Fract(float x) => x - MathF.Truncate(x);
    [MethodImpl(INLINE)] public static double Fract(double x) => x - Math.Truncate(x);

    #endregion

    #region DivRem

    // Notice: do not trust floating points
    // https://github.com/dotnet/runtime/issues/5213.
    // I tested is also valid with my implementation

    [MethodImpl(INLINE)]
    public static float ModF(float dividend, float divisor) => dividend - MathF.Truncate(dividend / divisor) * divisor;

    [MethodImpl(INLINE)]
    public static float DivRem(float dividend, float divisor, out float remainer)
    {            
        var quotient = dividend / divisor;
        //remainer = dividend % divisor;
        remainer = dividend - MathF.Truncate(quotient) * divisor;
        return quotient;
    }
    [MethodImpl(INLINE)]
    public static (float Quotient, float Remainder) DivRem(float dividend, float divisor)
    {
        var quotient = dividend / divisor;
        //remainer = dividend % divisor;
        return (quotient, dividend - MathF.Truncate(quotient) * divisor);
    }

    #endregion

}