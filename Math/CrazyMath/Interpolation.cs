using System;
using System.Runtime.CompilerServices;

using static Util.UtilShared;

namespace Util.MathC
{
    public static class Interpolation
    {
        // https://en.wikipedia.org/wiki/Smoothstep
        // https://sol.gfxile.net/interpolation/

        #region Smoothstep | https://iquilezles.org/articles/smoothsteps/
        // Inv = Inverse

        #region Polynomial Smoothstep

        [MethodImpl(INLINE)] public static float CubicSmoothstep(float x) => x * x * (3f - 2f * x);
        [MethodImpl(INLINE)] public static float CubicInvSmoothstep(float x) => (float)(0.5 - MathF.Sin(MathF.Asin(1f - 2f * x) / 3f));

        [MethodImpl(INLINE)] public static float QuarticSmoothstep(float x) => x * x * (2f - x * x);
        [MethodImpl(INLINE)] public static float QuarticInvSmoothstep(float x) => MathF.Sqrt(1f - MathF.Sqrt(1f - x));

        [MethodImpl(INLINE)] public static float QuinticSmoothstep(float x) => x * x * x * (x * (x * 6f - 15f) + 10f);

        #endregion

        #region Rational Smoothstep

        [MethodImpl(INLINE)] public static float RatQuadraticSmoothstep(float x) => x * x / (2f * x * x - 2f * x + 1f);
        [MethodImpl(INLINE)] public static float RatQuadraticInvSmoothstep(float x) => (x - MathF.Sqrt(x * (1f - x))) / (2f * x - 1f);

        [MethodImpl(INLINE)] public static float RatCubicSmoothstep(float x) => x * x * x / (3f * x * x - 3f * x + 1f);
        [MethodImpl(INLINE)]
        public static float RatCubicInvSmoothstep(float x)
        {
            var a = MathF.Pow(x, 1f / 3f);
            var b = MathF.Pow(1f - x, 1f / 3f);
            return a / (a + b);
        }

        [MethodImpl(INLINE)] public static float RationalSmoothstep(float x, float n) => MathF.Pow(x, n) / (MathF.Pow(x, n) + MathF.Pow(1f - x, n));
        [MethodImpl(INLINE)] public static float RationalInvSmoothstep(float x, float n) => RationalSmoothstep(x, 1f / n);

        #endregion

        #region Piecewise Smoothstep

        [MethodImpl(INLINE)] public static float PiecewiseQuadraticSmoothstep(float x) => (x < 0.5f) ? 2f * x * x : 2f * x * (2f - x) - 1f;
        [MethodImpl(INLINE)] public static float PiecewiseQuadraticInvSmoothstep(float x) => (x < 0.5f) ? MathF.Sqrt(0.5f * x) : 1f - MathF.Sqrt(0.5f - 0.5f * x);

        [MethodImpl(INLINE)] public static float PiecewisePolynomialSmoothstep(float x, float n) => (x < 0.5f) ? 0.5f * MathF.Pow(2f * x, n) : 1f - 0.5f * MathF.Pow(2f * (1f - x), n);
        [MethodImpl(INLINE)] public static float PiecewisePolynomialInvSmoothstep(float x, float n) => (x < 0.5f) ? 0.5f * MathF.Pow(2f * x, 1f / n) : 1f - 0.5f * MathF.Pow(2f * (1f - x), 1f / n);

        #endregion

        #region Trigonometric Smoothstep

        [MethodImpl(INLINE)] public static float TrigonometricSmoothstep(float x) => 0.5f - 0.5f * MathF.Cos(UtilMath.TAU_180 * x);
        [MethodImpl(INLINE)] public static float TrigonometricInvSmoothstep(float x) => MathF.Acos(1f - 2f * x) / UtilMath.TAU_180;

        #endregion

        #endregion

        #region IQ Funcs | https://iquilezles.org/articles/functions/

        public static float AlmostIdentity(float x, float m, float n)
        {
            if (x > m)
                return x;
            float a = 2f * n - m;
            float b = 2f * m - 3f * n;
            float t = x / m;
            return (a * t + b) * t * t + n;
        }

        public static float AlmostIdentity2(float x, float n) => MathF.Sqrt(x * x + n);

        public static float AlmostUnitIdentity(float x) => x * x * (2f - x);
        public static float IntegralSmoothstep(float x, float t)
        {
            if (x > t)
                return x - t / 2f;
            return x * x * x * (1f - x * 0.5f / t) / t / t;
        }

        public static float ExpImpulse(float x, float k)
        {
            float h = k * x;
            return h * MathF.Exp(1f - h);
        }

        public static float QuaImpulse(float x, float k) => 2f * MathF.Sqrt(k) * x / (1f + k * x * x);
        public static float PolyImpulse(float k, float n, float x) => n / (n - 1f) * MathF.Pow((n - 1f) * k, 1f / n) * x / (1f + k * MathF.Pow(x, n));

        public static float ExpSustainedImpulse(float x, float f, float k)
        {
            float s = Math.Max(x - f, 0f);
            return Math.Min(x * x / (f * f), 1f + 2f / f * s * MathF.Exp(-k * s));
        }

        public static float CubicPulse(float c, float w, float x)
        {
            x = Math.Abs(x - c);
            if (x > w)
                return 0f;
            x /= w;
            return 1f - x * x * (3f - 2f * x);
        }

        public static float ExpStep(float x, float k, float n) => MathF.Exp(-k * MathF.Pow(x, n));

        public static float Gain(float x, float k)
        {
            float a = 0.5f * MathF.Pow(2f * ((x < 0.5) ? x : 1f - x), k);
            return (x < 0.5f) ? a : 1f - a;
        }

        public static float Parabola(float x, float k) => MathF.Pow(4f * x * (1f - x), k);

        public static float Pcurve(float x, float a, float b)
        {
            float k = MathF.Pow(a + b, a + b) / (MathF.Pow(a, a) * MathF.Pow(b, b));
            return k * MathF.Pow(x, a) * MathF.Pow(1f - x, b);
        }

        public static float Sinc(float x, float k)
        {
            float a = UtilMath.TAU_180 * (k * x - 1f);
            return MathF.Sin(a) / a;
        }

        #endregion

    }
}
