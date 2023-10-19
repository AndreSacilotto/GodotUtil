using System.Runtime.CompilerServices;

namespace Util.MathExtra;

using number_t = System.Single;

public static class GeneratingFunction 
{
    /// <summary>equation: a * r ^ n <br/> sequence: a, ar, ar^2, ar^3 </summary>
    public static number_t GeometricSeries(number_t a, number_t r, number_t n)
    {
        if (r == 1)
            return a * n;
        return a * (MathF.Pow(r, n) - 1) / (r - 1);
    }

    #region Faulhaber

    // https://en.wikipedia.org/wiki/Faulhaber%27s_formula

    //[MethodImpl(INLINE)] public static number_t FaulhaberK0(number_t n) => n;
    [MethodImpl(INLINE)] public static number_t FaulhaberK1(number_t n) => (n * n + n) / 2;
    [MethodImpl(INLINE)] public static number_t FaulhaberK2(number_t n) => (n * n + n) * (2 * n + 1) / 6;
    [MethodImpl(INLINE)] public static number_t FaulhaberK3(number_t n) => n * n * (n + 1) * (n + 1) / 4;
    [MethodImpl(INLINE)] public static number_t FaulhaberK4(number_t n)
    {
        var n2 = n * n;
        return (n2 + n) * (2 * n + 1) * (3 * n2 + 3 * n - 1) / 30;
    }
    [MethodImpl(INLINE)] public static number_t FaulhaberK5(number_t n)
    {
        var n2 = n * n;
        var n4 = n2 * n2;
        return 2 * n4 * n2 + 6 * n4 * n + 5 * n4 - n2;
    }

    /// <summary>first degree polynomial: a + b * x</summary>
    [MethodImpl(INLINE)] 
    public static number_t FaulhaberLinear(number_t a, number_t b, number_t x) => 
        a * x + b * FaulhaberK1(x);

    /// <summary>second degree polynomial: a + b * x + c * x^2 </summary>
    [MethodImpl(INLINE)] 
    public static number_t FaulhaberQuad(number_t a, number_t b, number_t c, number_t x) => 
        FaulhaberLinear(a, b, x) + c * FaulhaberK2(x);

    /// <summary>third degree polynomial: a + b * x + c * x^2 + d * x^3 </summary>
    [MethodImpl(INLINE)] public static number_t FaulhaberCubic(number_t a, number_t b, number_t c, number_t d, number_t x) =>
        FaulhaberQuad(a, b, c, x) + d * FaulhaberK3(x);

    /// <summary>fourth degree polynomial: a + b * x + c * x^2 + d * x^3 + e * x^4 </summary>
    [MethodImpl(INLINE)]
    public static number_t FaulhaberQuat(number_t a, number_t b, number_t c, number_t d, number_t e, number_t x) =>
        FaulhaberCubic(a, b, c, d, x) + e * FaulhaberK4(x);

    /// <summary>fifth degree polynomial: a + b * x + c * x^2 + d * x^3 + e * x^4 + f * x^5 </summary>
    [MethodImpl(INLINE)]
    public static number_t FaulhaberQuint(number_t a, number_t b, number_t c, number_t d, number_t e, number_t f, number_t x) =>
        FaulhaberQuat(a, b, c, d, e, x) + f * FaulhaberK5(x);

    #endregion



}